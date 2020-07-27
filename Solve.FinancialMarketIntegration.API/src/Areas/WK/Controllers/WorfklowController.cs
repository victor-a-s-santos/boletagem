using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.WK.Models;
using Microsoft.AspNetCore.Authorization;
using Solve.FinancialMarketIntegration.API.Areas.Security.Services;
using Solve.FinancialMarketIntegration.API.Areas.WK.Entities;
using System.Linq.Expressions;
using Microsoft.Extensions.Caching.Memory;
using Solve.FinancialMarketIntegration.API.Areas.WK.Events.DomainEvents;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Exceptions;
using Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.Cache;
using Solve.FinancialMarketIntegration.API.Areas.WK.Events;
using Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess.Queries;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Controllers
{
    [Authorize]
    [Route("api/v1/wk/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase, IWorkflowService
    {
        private WorkflowDataContext context;
        private IAuthService authService;
        private WorkflowCache cache;
        private EventBus eventBus;

        public WorkflowController(WorkflowDataContext context, IAuthService authService, WorkflowCache cache, EventBus eventBus)
        {
            this.context = context;
            this.authService = authService;
            this.cache = cache;
            this.eventBus = eventBus;
        }

        // POST api/values
        [HttpPost]
        public async Task<int> Post([FromBody] WorkflowCreate body)
        {
            var result = await OpenAsync(body.WorkflowTypeId, body.Username);

            if (result == 0)
                throw new NotFoundException("Workflow not found");

            return result;
        }

        [HttpPost("approve")]
        public async Task<bool> Approve([FromBody] WorkflowExecuteStep body)
        {
            var result = await this.ApproveAsync(body.WorkflowId, body.Username, body.Comments);
            return result;
        }

        [HttpPost("reject")]
        public async Task<bool> Reject([FromBody] WorkflowExecuteStep body)
        {
            var result = await this.RejectAsync(body.WorkflowId, body.Username, body.Comments);
            return result;
        }

        /// <summary>
        /// Get workflow status.
        /// </summary>
        /// <returns>A dictionary with workflow status</returns>
        [HttpGet("status")]
        public async Task<ActionResult<Dictionary<int, string>>> GetStatus()
        {
            var status = context.WorkflowStatus
                        .Select(s => new Models.WorkflowStatus()
                        {
                            Id = s.Id,
                            Name = s.Name
                        })
                        .ToDictionary(w => w.Id, w => w.Name);

            return await Task.FromResult(status);
        }

        /// <summary>
        /// Remove a instance from workflow cache
        /// </summary>
        /// <param name="id">workflow id</param>
        /// <returns></returns>
        [HttpPost("remove-cache")]
        public ActionResult RemoveCache([FromRoute]int id)
        {
            cache.Remove(id);
            return Ok();
        }

        [NonAction]
        public Task<int> OpenAsync(int type, string userId)
        {
            return OpenAsync(type, userId, null);
        }

        [NonAction]
        public async Task<int> OpenAsync(int type, string userId, int? status)
        {
            var workflowType = await context.WorkflowTypes
                .Include(t => t.Steps)
                .ThenInclude(s => s.WorkflowStatus)
                .Include(t => t.Steps)
                .ThenInclude(s => s.Approvals)
                .SingleOrDefaultAsync(w => w.Id == type && w.IsActive);

            if (workflowType == null)
                return 0;

            var workflow = workflowType.CreateNewInstance();

            context.CurrentUser = userId;
            context.SaveChanges();

            workflowType.Configure(workflow, status);

            context.SaveChanges();

            return workflow.Id;
        }

        [NonAction]
        public async Task<int> CancelDraftAsync(int type, string userId)
        {
            var result = await OpenAsync(type, userId, (int)WorkflowStatuses.CancelledByAccountManager);
            return result;
        }

        [NonAction]
        /// <summary>
        /// Approve current workflow status
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="username"></param>
        /// <param name="comments"></param>
        /// <returns>
        /// Returns true if workflow is finished
        /// </returns>
        public async Task<bool> ApproveAsync(int workflowId, string userId, string comments = "")
        {
            var workflow = await context.Workflows.Include(t => t.CurrentStep)
                                            .ThenInclude(c => c.NextStepApproved)
                                            .ThenInclude(c => c.NextStepApproved.NextStepApproved)
                                            .Include(c => c.CurrentStep)
                                            .ThenInclude(c => c.NextStepApproved.NextStepRejected)
                                            .Include(t => t.CurrentStep)
                                            .ThenInclude(c => c.Approvals)
                                            .FirstOrDefaultAsync(w => w.Id == workflowId && w.IsFinished == false);

            if (workflow == null)
                throw new NotFoundException("Workflow not found");

            if (!authService.HasRoles(userId, workflow.CurrentStep.Approvals.Select(a => a.RoleId)))
                throw new ForbiddenException("UserId without permission to execute this approve step action");


            var previousStepId = workflow.CurrentStep.Id;
            context.CurrentUser = userId;
            var isFinished = workflow.Approve(userId, comments);

            context.SaveChanges();

            eventBus.Dispatch(new StepApprovedEvent(workflowId, previousStepId, workflow.CurrentStep.Id));

            return isFinished;
        }

        [NonAction]
        /// <summary>
        /// Reject current workflow status
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="username"></param>
        /// <param name="comments"></param>
        /// <returns>
        /// Returns true if workflow is finished
        /// </returns>
        public async Task<bool> RejectAsync(int workflowId, string userId, string comments = "")
        {
            var workflow = await context.Workflows.Include(t => t.CurrentStep)
                                            .ThenInclude(c => c.NextStepRejected)
                                            .ThenInclude(c => c.NextStepRejected.NextStepApproved)
                                            .Include(t => t.CurrentStep)
                                            .ThenInclude(c => c.NextStepRejected.NextStepRejected)
                                            .Include(t => t.CurrentStep)
                                            .ThenInclude(c => c.Approvals)
                                            .FirstOrDefaultAsync(w => w.Id == workflowId && w.IsFinished == false);

            if (workflow == null)
                throw new NotFoundException("Workflow not found");

            if (!authService.HasRoles(userId, workflow.CurrentStep.Approvals.Select(a => a.RoleId)))
                throw new ForbiddenException("UserId without permission to execute this reject step action");

            context.CurrentUser = userId;
            var previousStepId = workflow.CurrentStep.Id;
            var isFinsihed = workflow.Reject(userId, comments);

            context.SaveChanges();

            eventBus.Dispatch(new StepApprovedEvent(workflowId, previousStepId, workflow.CurrentStep.Id));
            return isFinsihed;
        }

        [NonAction]
        public async Task<WorkflowDetails> GetWorkflowDetailsAsync(int workflowId)
        {
            return await cache.GetAsync(workflowId);
        }

        [NonAction]
        public async Task<IEnumerable<WorkflowDetails>> GetWorkflowsDetailAsync(IEnumerable<int> workflowIds)
        {
            var list = new List<WorkflowDetails>();

            foreach (var id in workflowIds)
            {
                var workflow = await cache.GetAsync(id);
                if (workflow != null)
                {
                    list.Add(workflow);
                }
            }

            return list;
        }

        [NonAction]
        public IEnumerable<WorkflowDetails> GetWorkflowsDetail(IEnumerable<int> workflowIds)
        {
            var task = GetWorkflowsDetailAsync(workflowIds);
            task.Wait();

            return task.Result;
        }

        [NonAction]
        public IEnumerable<WorkflowDetails> GetWorkflowsBy(int statusId, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return context.Workflows.Where(w => w.CurrentStep.WorkflowStatus.Id == statusId && w.StartDate >= startDate && w.StartDate <= endDate.EndOfDay())
                                    .Select(WorkflowQueries.WorkflowToStepProjection)
                                    .ToArray();
        }


    }
}

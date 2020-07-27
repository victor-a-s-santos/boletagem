using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Security.Services;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Events;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Events.DomainEvents;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Exceptions;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Filters;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using Solve.FinancialMarketIntegration.API.Areas.WK.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Controllers
{
    [Authorize]
    [Route("api/v1/tickets/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private TicketDataContext Context { get; set; }
        private IWorkflowService WorkflowService { get; set; }
        private IAuthService AuthService { get; set; }
        private IAccountManagerService AccountManagerService { get; set; }


        public TicketController(
            TicketDataContext context,
            IWorkflowService workflowService,
            IAuthService authService,
            IAccountManagerService accountManagerService
        )
        {
            this.Context = context;
            this.WorkflowService = workflowService;
            this.AuthService = authService;
            this.AccountManagerService = accountManagerService;
        }

        [HttpGet]
        [Route("summary")]
        public async Task<ActionResult<IEnumerable<StatusGroup>>> GetSummary(
            [FromQuery]DateTimeOffset? startDate,
            [FromQuery]DateTimeOffset? endDate
        )
        {
            var query = Context.Ticket.Select(t => t);

            if (startDate != null)
            {
                query = query.Where(t => t.OperationDate.Date >= startDate.Value.Date);
            }

            if (endDate != null)
            {
                query = query.Where(t => t.OperationDate.Date <= endDate.Value.AddDays(1).Date);
            }

            var accountManager = this.AccountManagerService.GetAccountManager(User.Identity.Name);
            if (accountManager != null)
            {
                query = query.Where(t => t.AccountManager.Id == accountManager.Id);
            }

            var tickets = (await query
                .Select(t => new
                {
                    t.WorkflowId,
                    TicketTypeId = t.Type.Id
                })
                .ToListAsync());

            if (tickets.Count == 0)
            {
                return Ok(new StatusGroup[] { });
            }

            var workflowStatus = (await WorkflowService.GetWorkflowsDetailAsync(
                tickets
                    .Where(t => t.WorkflowId != null)
                    .Select(t => (int)t.WorkflowId)
            )).ToDictionary(kvp => kvp.WorkflowId, kvp => kvp);

            var workflowByType = tickets
                .Select(t => new
                {
                    t.TicketTypeId,
                    StatusId = t.WorkflowId == null ? null : (int?)workflowStatus[(int)t.WorkflowId].StatusId
                });

            var result = workflowByType
                .GroupBy(wfs => wfs.StatusId)
                .Select(grp => new StatusGroup()
                {
                    StatusId = grp.Key,
                    Tickets = grp.GroupBy(
                        g => g.TicketTypeId,
                        (g, t) => new TicketCount()
                        {
                            TicketTypeId = g,
                            Count = t.Count()
                        })
                });

            return Ok(result);
        }

        [HttpPost]
        [Route("{id}/approve")]
        [WorkflowActionExceptionFilter]
        public async Task<ActionResult> Approve([FromServices]EventBus eventBus, int id, string comments)
        {
            var workflowId = Context.Ticket.Where(t => t.Id == id && t.WorkflowId.HasValue)
                                        .Select(t => t.WorkflowId.Value)
                                        .FirstOrDefault();

            var previousStatus = await WorkflowService.GetWorkflowDetailsAsync(workflowId);
            var isFinished = await WorkflowService.ApproveAsync(workflowId, User.Identity.Name, comments);

            eventBus.Dispatch(new TicketStatusChangedEvent(id,
                isApproved: true,
                previousStatus: previousStatus.StatusDescription,
                comments: comments,
                user: User.Identity.Name));

            return Ok();
        }

        [HttpPost]
        [Route("{id}/reject")]
        [WorkflowActionExceptionFilter]
        public async Task<ActionResult> Reject([FromServices]EventBus eventBus, int id, TicketComment ticketComment)
        {
            var workflowId = Context.Ticket.Where(t => t.Id == id && t.WorkflowId.HasValue)
                                        .Select(t => t.WorkflowId.Value)
                                        .FirstOrDefault();

            var previousStatus = await WorkflowService.GetWorkflowDetailsAsync(workflowId);
            var isFinished = await WorkflowService.RejectAsync(workflowId, User.Identity.Name, ticketComment.comments);

            eventBus.Dispatch(new TicketStatusChangedEvent(id,
                isApproved: false,
                previousStatus: previousStatus.StatusDescription,
                comments: ticketComment.comments,
                user: User.Identity.Name));

            return Ok();
        }

        [HttpPost]
        [Route("{id}/advance-step")]
        public async Task<ActionResult> AdvanceStep([FromRoute]int id, string comments)
        {
            var workflowId = Context.Ticket.Where(t => t.Id == id && t.WorkflowId.HasValue)
                                        .Select(t => t.WorkflowId.Value)
                                        .FirstOrDefault();

            var workflowDetails = await WorkflowService.GetWorkflowDetailsAsync(workflowId);

            // TODO: Resolve this problem
            if (workflowDetails?.StatusId != (int)WorkflowStatuses.WaitingSettlement)
            {
                return BadRequest();
            }

            await WorkflowService.ApproveAsync(workflowId, User.Identity.Name, comments ?? "");

            return Ok();
        }

        [HttpGet]
        [Route("{id}/history")]
        public async Task<ActionResult<IEnumerable<TicketHistoryModel>>> WorkflowHistory([FromRoute] int id)
        {
            var result = await Context.Ticket.Where(t => t.Id == id)
                                       .SelectMany(t => t.History)
                                       .Select(t => new TicketHistoryModel
                                       {
                                           Id = t.Id,
                                           Moment = t.Moment,
                                           Details = t.Details ?? t.Event.Description,
                                           Comments = t.Comments,
                                           AuthorUserId = t.User
                                       })
                                       .OrderBy(t => t.Id)
                                       .ToArrayAsync();

            if (result == null)
            {
                return NotFound();
            }

            var users = result.Select(s => s.AuthorUserId).ToList();

            if (users.Count() > 0)
            {
                var usernames = AuthService.GetUserNames(users).ToDictionary(kvp => kvp.User, kvp => kvp.Name);

                foreach (var history in result)
                {
                    history.AuthorUserName = usernames[history.AuthorUserId];
                }
            }

            return result;
        }

        [HttpGet]
        [Route("version")]
        public ActionResult ApiVersion()
        {
            var version = typeof(TicketController).GetTypeInfo().Assembly
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

            return Ok(new { Version = version });
        }
    }

    public class StatusGroup
    {
        public int? StatusId { get; set; }
        public IEnumerable<TicketCount> Tickets { get; set; }

    }

    public class TicketCount
    {
        public short TicketTypeId { get; set; }
        public int Count { get; set; }
    }

    public class TicketComment
    {
        public string comments { get; set; }
    }

    public class TicketHistoryModel
    {
        public long Id { get; set; }
        public DateTimeOffset Moment { get; set; }
        public string Details { get; set; }
        public string Comments { get; set; }
        public string AuthorUserId { get; set; }
        public string AuthorUserName { get; set; }
    }
}

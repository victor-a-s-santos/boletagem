using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Controllers
{
    //[Authorize]
    [Route("api/v1/wk/[controller]")]
    [ApiController]
    public class WorkflowTypeController : ControllerBase
    {
        private WorkflowDataContext context;

        public WorkflowTypeController(WorkflowDataContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var result = context.WorkflowTypes.Where(t => t.Id == id)
                                        .Select(t => new
                                        {
                                            t.Id,
                                            t.Name,
                                            steps = t.Steps.Select(s => new
                                            {
                                                s.Id,
                                                status = new {
                                                    s.WorkflowStatus.Id,
                                                    s.WorkflowStatus.Name
                                                },
                                                s.IsFirstStep,
                                                approvedStepId = s.NextStepApproved != null ? s.NextStepApproved.Id : 0,
                                                rejectedStepId = s.NextStepRejected != null ? s.NextStepRejected.Id : 0,
                                                approvals = s.Approvals.Select(a => new {
                                                    name = a.Approval.Name,
                                                    role = a.RoleId
                                                })
                                            }).OrderByDescending(s => s.IsFirstStep).ToList()
                                        }).FirstOrDefault();
            return Ok(result);
        }
    }
}
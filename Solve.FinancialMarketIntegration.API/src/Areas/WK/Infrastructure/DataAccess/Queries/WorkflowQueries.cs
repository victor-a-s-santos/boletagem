using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using Solve.FinancialMarketIntegration.API.Areas.WK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess.Queries
{
    public class WorkflowQueries
    {
        public static Expression<Func<Workflow, WorkflowDetails>> WorkflowToStepProjection
        {
            get
            {
                return w => new WorkflowDetails
                {
                    WorkflowId = w.Id,
                    StartDate = w.StartDate,
                    EndDate = w.EndDate,
                    StatusId = w.CurrentStep.WorkflowStatus.Id,
                    StatusDescription = w.CurrentStep.WorkflowStatus.Name,
                    StatusRequiredRoles = w.CurrentStep.Approvals.Select(a => a.RoleId),
                    CanProceed = w.IsFinished == false,
                };
            }
        }
    }
}

using Solve.FinancialMarketIntegration.API.Areas.WK.Events.DomainEvents;
using Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.Cache;
using Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Events.Handlers
{
    public class WorkflowStepChangedHandler : IEventHandler<StepApprovedEvent>
    {
        private readonly WorkflowCache cache;

        public WorkflowStepChangedHandler(WorkflowCache cache)
        {
            this.cache = cache;
        }

        public void Execute(StepApprovedEvent e)
        {
            if (e.PreviousStepId == e.CurrentStepId) return;

            cache.RefreshAsync(e.WorkflowId);
        }
    }
}

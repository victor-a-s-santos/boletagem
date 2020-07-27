using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Events.DomainEvents
{
    public class StepApprovedEvent : StepChangedEvent
    {
        public StepApprovedEvent(int workflowId, int previousStepId, int currentStepId)
            : base(workflowId, previousStepId, currentStepId)
        {
        }
    }
}

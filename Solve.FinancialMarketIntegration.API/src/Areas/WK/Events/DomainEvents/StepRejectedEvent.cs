using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Events.DomainEvents
{
    public class StepRejectedEvent : StepChangedEvent
    {
        public StepRejectedEvent(int workflowId, int previousStepId, int currentStepId)
            : base(workflowId, previousStepId, currentStepId)
        {
        }
    }
}

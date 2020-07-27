using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Events.DomainEvents
{
    public abstract class StepChangedEvent : IEvent
    {
        public StepChangedEvent(int workflowId, int previousStepId, int currentStepId)
        {
            WorkflowId = workflowId;
            PreviousStepId = previousStepId;
            CurrentStepId = currentStepId;
        }

        public int WorkflowId { get; }
        public int PreviousStepId { get; }
        public int CurrentStepId { get; }
    }
}

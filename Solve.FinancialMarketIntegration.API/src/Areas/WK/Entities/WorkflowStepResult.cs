using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Entities
{
    public class WorkflowStepResult : Entity
    {
        public WorkflowStepResult()
        {
            Approvals = new List<WorkflowApprovalStepResult>();
        }

        public WorkflowStepResult NextStepApproved { get; set; }
        public WorkflowStepResult NextStepRejected { get; set; }
        public WorkflowStatus WorkflowStatus { get; set; }
        public WorkflowApprovalStep Origin { get; set; }
        public ICollection<WorkflowApprovalStepResult> Approvals { get; set; }

        public bool IsFirstStep { get; set; }

        public bool HasNextStep()
        {
            return NextStepApproved != null || NextStepRejected != null;
        }
    }

}

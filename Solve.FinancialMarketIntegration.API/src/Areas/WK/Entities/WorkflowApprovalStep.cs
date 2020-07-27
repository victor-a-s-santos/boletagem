using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Entities
{
    public class WorkflowApprovalStep : Entity
    {
        public WorkflowApprovalStep NextStepApproved { get; set; }
        public WorkflowApprovalStep NextStepRejected { get; set; }

        public WorkflowStatus WorkflowStatus { get; set; }

        public ICollection<WorkflowApprovalRole> Approvals { get; set; }

        public bool IsFirstStep { get; set; }

        public bool IsActive { get; set; }

        public WorkflowStepResult CreateNewInstance()
        {
            var result = new WorkflowStepResult
            {
                IsFirstStep = this.IsFirstStep,
                WorkflowStatus = this.WorkflowStatus,
                Origin = this,
                Approvals = this.Approvals.Select(t => new WorkflowApprovalStepResult
                {
                    Approval = t.Approval,
                    RoleId = t.RoleId
                }).ToArray()
            };

            return result;
        }
    }
}

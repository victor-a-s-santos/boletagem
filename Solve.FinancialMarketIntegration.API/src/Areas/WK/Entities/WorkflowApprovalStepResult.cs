using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Entities
{
    public class WorkflowApprovalStepResult : Entity
    {
        public WorkflowApproval Approval { get; set; }
        public int RoleId { get; set; }
        public bool? IsApproved { get; set; }
        public string Comments { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Entities
{
    public class WorkflowApproval : Entity
    {
        public string Name { get; set; }
    }

    public enum WorkflowApprovals
    {
        AccountManagers = 1,
        AdminFiduci = 2,
        Open = 3,
        Detainer = 4,
        AdminMiddle = 5,
    }
}

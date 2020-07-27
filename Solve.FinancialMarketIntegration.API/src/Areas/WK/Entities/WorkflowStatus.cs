using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Entities
{
    public class WorkflowStatus : Entity<short>
    {
        public string Name { get; set; }
    }

    public enum WorkflowStatuses
    {
        PendingAdmApproval = 1,
        CancelledByAdm = 2,
        WaitingSettlement = 3,
        InSettlement = 4,
        Settled = 5,
        CancelledBySettlement = 6,
        WaitingDetainerSettlement = 7,
        InDetainerSettlement = 8,
        CancelledByDetainer = 9,
        WaitingSettlementMiddle = 10,
        InMiddleSettlement = 11,
        CancelledByMiddle = 12,
        WaitingVoiceRecord = 13,
        InVoiceRecord = 14,
        CancelledByAccountManager = 15,
        Completed = 16,
        PendingAccountManagerApproval = 17
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Services
{
    public enum Roles
    {
        CreateQuotaTicket = 1,
        CreatePrivateFixedIncomeTicket = 2,
        CreatePublicFixedIncomeTicket = 3,
        CreateFuturesTicket = 4,
        CreateMarginTicket = 5,
        CreateContractedTicket = 6,
        CreateCurrencyTermTicket = 7,
        CreateVariableIncomeTicket = 8,
        CreateSwapCETIPTicket = 9,
        ApproveAdmPendingTickets = 10,
        ApproveOpenAwaitingSettlement = 11,
        ApproveOpenActiveSettlement = 12,
        ApproveMiddleAwaitingSettlement = 13,
        ApproveMiddleActiveSettlement = 14,
        ApproveCustodyAwaitingSettlement = 15,
        ApproveCustodyActiveSettlement = 16,
        ViewApprovalHistory = 17,
        ViewQuotaMonitor = 18,
        ViewPrivateFixedIncomeMonitor = 19,
        ViewPublicFixedIncomeMonitor = 20,
        ViewFuturesMonitor = 21,
        ViewMarginMonitor = 22,
        ViewContractedMonitor = 23,
        ViewCurrencyTermMonitor = 24,
        ViewVariableIncomeMonitor = 25,
        ViewSwapCETIPMonitor = 26,
        ApproveAccountManagerPendingTicket = 27,
        CreateMasterUser = 28,
        CreateSubordinateUser = 29,
        ChangeDefaultLimitTime = 30,
        ChangeLimitTime = 31
    }
}

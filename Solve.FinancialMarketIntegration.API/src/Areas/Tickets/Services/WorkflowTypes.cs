using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Services
{
    public enum WorkflowTypes
    {
        FundQuota = 1,
        PrivateFixedIncome = 2,
        PublicFixedIncome = 3,
        Contracted = 4,
        SwapCetip = 5,
        Margin = 6,
        CurrencyTerm = 7,
        FundQuotaTED = 8,
        FundQuotaCetipVoice = 9,
        Futures = 10,
        MarginVariableIncome = 11,
        MarginCurrency = 12,
        VariableIncome = 13,
        VariableIncomeByManager = 14

    }
}

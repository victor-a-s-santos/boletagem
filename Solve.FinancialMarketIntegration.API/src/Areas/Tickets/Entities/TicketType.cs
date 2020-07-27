using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public enum TicketTypes
    {
        FundQuota = 1,
        PrivateFixedIncome = 2,
        PublicFixedIncome = 3,
        Contracted = 4,
        Futures = 5,
        SwapCetip = 6,
        Margin = 7,
        CurrencyTerm = 8,
        VariableIncome = 9
    }

    public class TicketType
    {
        public short Id { get; set; }
        public string Name { get; set; }
    }
}

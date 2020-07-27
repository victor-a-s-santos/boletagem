using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public enum SettlementTypes
    {
        CETIP = 1,
        TED = 2,
        Term = 3,
        InCash = 4
    }

    public class SettlementType
    {
        public short Id { get; set; }
        public string Name { get; set; }
    }
}

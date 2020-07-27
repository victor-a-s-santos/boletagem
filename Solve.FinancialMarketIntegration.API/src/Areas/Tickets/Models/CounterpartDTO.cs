using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
    public class CounterpartDTO
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string ClearingAccount { get; set; }
        public string Command { get; set; }
    }
}

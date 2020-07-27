using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Entities
{
    public class PasswordSettings
    {
        public int HistoryAge { get; set; }
        public int ExpirationDays { get; set; }
        public int AttemptsLimit { get; set; }
    }
}

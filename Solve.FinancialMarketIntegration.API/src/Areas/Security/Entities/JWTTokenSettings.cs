using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Entities
{
    public class JWTTokenSettings
    {
        public string Key { get; set; }
        public int ExpirationDays { get; set; }
    }
}
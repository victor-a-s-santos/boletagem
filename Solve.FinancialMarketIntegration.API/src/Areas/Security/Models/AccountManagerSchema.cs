using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Models
{
    public class AccountManagerSchema
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
    }
}
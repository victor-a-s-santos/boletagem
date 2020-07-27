using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Entities
{
    public class UserPassword
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PasswordHash { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}

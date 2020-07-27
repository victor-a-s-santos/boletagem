using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Models
{
    public class UserAuthentication
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public bool isAccountManager {get;set;}
        public DateTimeOffset TokenExpiration { get; set; }
    }
}

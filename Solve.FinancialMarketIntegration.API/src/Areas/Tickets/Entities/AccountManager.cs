using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class AccountManager : Entity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string SacCode { get; set; }
        public IList<UserAccountManager> UsersAccountManagers { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class UserAccountManager : Entity
    {
        public string UserIdentifier { get; set; }
        public int AccountManagerId { get; set; }
        public AccountManager AccountManager { get; set; }
        public bool IsMaster { get; set; }
    }
}

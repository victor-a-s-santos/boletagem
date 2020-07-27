using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class AccountManagerFund
    {
        public int AccountManagerId { get; set; }
        public AccountManager AccountManager { get; set; }

        public int FundId { get; set; }
        public Fund Fund { get; set; }
    }
}

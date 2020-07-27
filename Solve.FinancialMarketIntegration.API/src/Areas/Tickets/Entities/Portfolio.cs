using System;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class Portfolio
    {
        public Portfolio() { }

        public Portfolio(string name, string code, string document, string account, string clearingAccount, string bank, string branch)
        {
            this.Name = name;
            this.Code = code;
            this.Document = document;
            this.Bank = bank;
            this.Branch = branch;
            this.Account = account;
            this.ClearingAccount = clearingAccount;
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public string Document { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string Account { get; set; }
        public string ClearingAccount { get; set; }

    }
}
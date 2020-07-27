using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class TicketVariableIncome : Ticketable
    {
        public string BrokerCode { get; set; }
        public DateTimeOffset? StockExchangeDate { get; set; }
        public string ClientCode { get; set; }
        public string ClientCodeDigit { get; set; }
        public decimal SellTotal { get; set; }
        public decimal BuyTotal { get; set; }
        public IList<TicketVariableIncomeItem> LineItems { get; set; }
    }
}

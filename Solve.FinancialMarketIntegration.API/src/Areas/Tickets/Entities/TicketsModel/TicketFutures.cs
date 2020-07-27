using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class TicketFutures : Ticketable
    {
        public OperationType OperationType { get; set; }
        public long? Amount { get; set; }
        public decimal? UnitPrice { get; set; }
        public DateTimeOffset? TradingDate { get; set; }
        public decimal? PercentageDiscount { get; set; }
        public string PaperCode { get; set; }
        public string PaperSerie { get; set; }
        public string Broker { get; set; }
        public string BrokerCode { get; set; }
        public string BrokerAccount { get; set; }
        public string BrokerDocument { get; set; }
    }
}
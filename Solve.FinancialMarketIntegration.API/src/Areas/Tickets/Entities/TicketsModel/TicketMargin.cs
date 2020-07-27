using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class TicketMargin : Ticketable
    {
        public OperationType OperationType { get; set; }
        public MarketType MarketType { get; set; }
        public decimal? Amount { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? OperationValue { get; set; }
        public string SecurityType { get; set; }
        public string SecurityName { get; set; }
        public string SecurityCode { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public DateTimeOffset? PurchaseDate { get; set; }
        public decimal? Quotation { get; set; }        
        public string Asset { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }        
        public string Account { get; set; }
        public Counterpart Counterpart { get; set; }
        public string CounterpartBrokerAccount { get; set; }    
        public string Index { get; set; }
        public decimal? IndexPercent { get; set; }
        public string Issuer { get; set; }
        public int? IndexBase { get; set; }
        public int? InterestType { get; set; }    
    }
}
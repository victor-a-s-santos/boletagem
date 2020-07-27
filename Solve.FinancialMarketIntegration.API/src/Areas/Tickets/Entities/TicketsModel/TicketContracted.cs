using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class TicketContracted : Ticketable
    {
        public OperationType OperationType { get; set; }
        public decimal? OperationValue { get; set; }
        public long? Amount { get; set; }
        public decimal? UnitPriceOutward { get; set; }
        public decimal? UnitPriceReturn { get; set; }
        public DateTimeOffset? ReturnDate { get; set; }
        public decimal? ValueOutward { get; set; }
        public decimal? ValueReturn { get; set; }
        public string Security { get; set; }
        public string SecurityId { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; }
        public decimal? IssueTax { get; set; }
        public Counterpart Counterpart { get; set; }
    }
}

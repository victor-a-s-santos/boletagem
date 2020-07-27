using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class TicketPublicFixedIncome : Ticketable
    {
        public OperationType OperationType { get; set; }
        public SettlementType SettlementType { get; set; }
        public long? Amount { get; set; }
        public decimal? UnitPrice { get; set; }
        public DateTimeOffset? SettlementDate { get; set; }
        public DateTimeOffset? AcquisitionDate { get; set; }
        public decimal? OperationValue { get; set; }
        public string Security { get; set; }
        public string SecurityId { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; }
        public decimal? IssueTax { get; set; }
        public DateTimeOffset? IssueDate { get; set; }
        public Counterpart Counterpart { get; set; }
    }
}

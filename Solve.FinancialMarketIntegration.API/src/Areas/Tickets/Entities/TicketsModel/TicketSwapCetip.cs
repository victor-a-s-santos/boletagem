using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class TicketSwapCetip : Ticketable
    {
        public decimal OperationValue { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public string Command { get; set; }
        public string ActiveIndex { get; set; }
        public decimal ActiveIndexPercent { get; set; }
        public decimal ActiveIndexTax { get; set; }
        public decimal ActiveIndexBase { get; set; }
        public decimal ActiveInterestType { get; set; }
        public string PassiveIndex { get; set; }
        public decimal PassiveIndexPercent { get; set; }
        public decimal PassiveIndexTax { get; set; }
        public decimal PassiveIndexBase { get; set; }
        public decimal PassiveInterestType { get; set; }
        public Counterpart Counterpart { get; set; }
    }
}

using System;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class TicketSignoff : Entity
    {
        public TicketTypes TicketTypeId { get; set; }
        public TimeSpan TimeLimit { get; set; }
        public string Type { get; set; }
    }
}
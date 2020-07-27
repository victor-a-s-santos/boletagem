using System;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class TicketSignoffRequest : Entity
    {
        public TicketSignoff TicketSignoff { get; set; }
        public TimeSpan TimeLimit { get; set; }
        public string Justificative { get; set; }
    }
}
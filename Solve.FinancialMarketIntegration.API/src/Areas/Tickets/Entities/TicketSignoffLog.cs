using System;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class TicketSignoffLog
    {
        public int Id { get; set; }
        public TicketSignoff TicketSignoff { get; set; }
        public TimeSpan TimeLimit { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string CreationUser { get; set; }
    }
}
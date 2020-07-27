using System;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
    public class TicketSignoffLogModel
    {
        public int Id { get; set; }
        public int TicketSignoffId { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string CreationUser { get; set; }
        public TimeSpan TimeLimit { get; set; }
    }
}
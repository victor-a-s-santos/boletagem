using System;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
    public class TicketSignoffHistoryModel
    {
        public int Id { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public string CreationUser { get; set; }

        public int TicketSignoffId { get; set; }

        public TimeSpan TimeLimit { get; set; }

        public string Justificative { get; set; }
    }
}
using System;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
    public class TicketSignoffResumeModel
    {
        public int Id { get; set; }

        public TicketTypes TicketTypeId { get; set; }

        public string Type { get; set; }

        public TimeSpan DefaultLimitTime { get; set; }

        public TimeSpan? CurrentLimitTime { get; set; }
    }
}
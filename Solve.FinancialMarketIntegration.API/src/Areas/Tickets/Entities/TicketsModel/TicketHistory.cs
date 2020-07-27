using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class TicketHistory
    {
        public long Id { get; set; }
        public DateTimeOffset Moment { get; set; }
        public EventType Event { get; set; }

        public string Details { get;set;}

        public string Comments { get; set; }
        public string User { get; set; }
    }
}
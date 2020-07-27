using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public enum EventTypes
    {
        TicketCancelled = 1,
        TicketForwarded = 2,
        TicketRegistered = 3,
        TicketStatusChaged = 4
    }


    public class EventType
    {
        public short Id { get; set; }
        public string Description { get; set; }
    }
}

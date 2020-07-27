using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Events.DomainEvents
{
    public class TicketForwardedEvent : TicketChangedEvent
    {
        public TicketForwardedEvent(int ticketId, string user)
        : base(ticketId, user) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Events.DomainEvents
{
    public class TicketRegisteredEvent : TicketChangedEvent
    {
        public TicketRegisteredEvent(int ticketId, string user)
        : base(ticketId, user) { }
    }
}

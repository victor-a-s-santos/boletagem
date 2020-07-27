using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Events.DomainEvents
{
    public abstract class TicketChangedEvent : IEvent
    {
        public TicketChangedEvent(int ticketId, string user)
        {
            this.TicketId = ticketId;
            this.User = user;
        }

        public int TicketId { get; private set; }
        public string User { get; private set; }
    }
}

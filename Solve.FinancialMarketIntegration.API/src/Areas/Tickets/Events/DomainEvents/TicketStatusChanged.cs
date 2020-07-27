using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Events.DomainEvents
{
    public class TicketStatusChangedEvent : TicketChangedEvent
    {
        public TicketStatusChangedEvent(int ticketId, bool isApproved, string previousStatus, string comments, string user)
        : base(ticketId, user)
        {
            PreviousStatus = previousStatus;
            Comments = comments;
            IsApproved = isApproved;
        }

        public bool IsApproved { get; private set; }
        public string PreviousStatus { get; private set; }
        public string Comments { get; private set; }
    }
}

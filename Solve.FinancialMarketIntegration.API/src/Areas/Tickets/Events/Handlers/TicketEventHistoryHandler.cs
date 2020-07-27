using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Events.DomainEvents;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Events.Handlers
{
    public class TicketEventHistoryHandler :
        IEventHandler<TicketCancelledEvent>,
        IEventHandler<TicketForwardedEvent>,
        IEventHandler<TicketRegisteredEvent>,
        IEventHandler<TicketStatusChangedEvent>
    {
        private TicketDataContext context;

        public TicketEventHistoryHandler(TicketDataContext context)
        {
            this.context = context;
        }

        public void Execute(TicketCancelledEvent e) => AddEventToHistory(e, EventTypes.TicketCancelled);
        public void Execute(TicketForwardedEvent e) => AddEventToHistory(e, EventTypes.TicketForwarded);
        public void Execute(TicketRegisteredEvent e) => AddEventToHistory(e, EventTypes.TicketRegistered);
        
        public void Execute(TicketStatusChangedEvent e)
        {
            var ticket = context.Ticket.Find(e.TicketId);
            var eventType = context.EventTypes.Find((short)EventTypes.TicketStatusChaged);

            var statusAction = e.IsApproved ? "Aprovado" : "Cancelado";

            ticket.History.Add(new TicketHistory 
            {
                Moment = DateTimeOffset.Now, 
                Event = eventType, 
                User = e.User,
                Details = $"O status \"{e.PreviousStatus}\" foi {statusAction}",
                Comments = e.Comments
            });

            context.SaveChanges();
        }

        private void AddEventToHistory(TicketChangedEvent _event, EventTypes type)
        {
            var ticket = context.Ticket.Find(_event.TicketId);
            var eventType = context.EventTypes.Find((short)type);

            ticket.History.Add(new TicketHistory { Moment = DateTimeOffset.Now, Event = eventType, User = _event.User });
            context.SaveChanges();
        }
    }
}

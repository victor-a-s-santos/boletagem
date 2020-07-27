using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Events
{
    public class EventBus
    {
        private IServiceProvider service;

        public EventBus(IServiceProvider service)
        {
            this.service = service;
        }

        public void Dispatch<T>(T _event)
            where T : IEvent
        {
            var handlers = service.GetServices<IEventHandler<T>>();

            foreach (var handler in handlers)
            {
                handler.Execute(_event);
            }
        }
    }
}

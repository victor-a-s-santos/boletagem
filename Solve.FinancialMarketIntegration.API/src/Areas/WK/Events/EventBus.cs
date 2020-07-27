using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Events
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
            var handlers = service.GetSafeServices<IEventHandler<T>>();

            foreach (var handler in handlers)
            {
                handler.Execute(_event);
            }
        }
    }

    public static class ServiceProviderExtensionMethods
    {
        public static IEnumerable<T> GetSafeServices<T>(this IServiceProvider provider)
        {
            try
            {
                return provider.GetServices<T>();
            }
            catch (Exception)
            {
                return new T[] { };
            }
        }
    }
}

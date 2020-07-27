using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Events
{
    public interface IEventHandler<T> where T : IEvent
    {
        void Execute(T e);
    }
}

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Services
{
    public interface ITicketBulkService<T> where T : ITicketModel
    {
        Task<IList<T>> RegisterNewBulkAsync(string username, IList<T> ticketData);
    }
}

using System;
using System.Threading.Tasks;
using TicketEntities = Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Files.Services
{
    public interface IFileService
    {
        Task<bool> ProcessRVAsync(string fileBase64, int offset, string username);
    }
}

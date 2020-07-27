using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public interface ITicketable
    {
        Ticket Ticket { get; }
    }
}
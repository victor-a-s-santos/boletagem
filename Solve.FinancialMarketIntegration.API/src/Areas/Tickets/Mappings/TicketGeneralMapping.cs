using AutoMapper;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities.Enums;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Mappings
{
    public class TicketGeneralMapping : Profile
    {
        public TicketGeneralMapping()
        {
            CreateMap<Counterpart, CounterpartDTO>();
            CreateMap<CounterpartDTO, Counterpart>();

            CreateMap<Portfolio, PortfolioModel>();
            CreateMap<PortfolioModel, Portfolio>();

            CreateMap<Currency, int>().ConvertUsing(c => (int)c);
            CreateMap<int, Currency>().ConvertUsing(c => (Currency)c);
        }
    }
}

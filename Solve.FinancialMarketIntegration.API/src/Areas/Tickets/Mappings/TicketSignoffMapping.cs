using AutoMapper;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace src.Areas.Tickets.Mappings
{
    public class TicketSignoffMapping : Profile
    {
        public TicketSignoffMapping()
        {
            CreateMap<TicketSignoff, TicketSignoffModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.TicketTypeId, o => o.MapFrom(s => s.TicketTypeId))
                .ForMember(d => d.TimeLimit, o => o.MapFrom(s => s.TimeLimit))
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
                .ReverseMap();
        }
    }
}
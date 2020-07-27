using AutoMapper;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace src.Areas.Tickets.Mappings
{
    public class TicketSignoffRequestMapping : Profile
    {
        public TicketSignoffRequestMapping()
        {
            CreateMap<TicketSignoffRequest, TicketSignoffRequestModel>()
                .ForMember(d => d.TicketSignoffId, o => o.MapFrom(s => s.TicketSignoff.Id))
                .ForMember(d => d.TimeLimit, o => o.MapFrom(s => s.TimeLimit))
                .ForMember(d => d.Justificative, o => o.MapFrom(s => s.Justificative))
                .ReverseMap();
        }
    }
}
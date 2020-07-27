using AutoMapper;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace src.Areas.Tickets.Mappings
{
    public class TicketSignofLogModelMapping : Profile
    {
        public TicketSignofLogModelMapping()
        {
            CreateMap<TicketSignoffLog, TicketSignoffLogModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.TicketSignoffId, o => o.MapFrom(s => s.TicketSignoff.Id))
                .ForMember(d => d.TimeLimit, o => o.MapFrom(s => s.TimeLimit))
                .ForMember(d => d.CreationDate, o => o.MapFrom(s => s.CreationDate))
                .ForMember(d => d.CreationUser, o => o.MapFrom(s => s.CreationUser))
                .ReverseMap();
        }
    }
}
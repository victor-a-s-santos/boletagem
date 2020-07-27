using System.Collections.Generic;
using AutoMapper;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace src.Areas.Tickets.Mappings
{
    public class TicketVariableIncomeItemMapping : Profile
    {
        public TicketVariableIncomeItemMapping()
        {
            // CreateMap<IEnumerable<TicketVariableIncomeItem>, IEnumerable<TicketVariableIncomeItensModel>>()
            //     .ReverseMap();
            
            CreateMap<TicketVariableIncomeItem, TicketVariableIncomeItemModel>()
                .ForMember(d => d.IdTicket, o => o.MapFrom(t => t.TicketId))
                .ForMember(d => d.BuySell, o => o.MapFrom(t => t.BuyOrSell));

            CreateMap<TicketVariableIncomeItemModel, TicketVariableIncomeItem>()
                .ForMember(o => o.TicketId, d => d.Ignore())
                .ForMember(o => o.BuyOrSell, d => d.MapFrom(t => t.BuySell))
                .ForMember(o => o.TicketVariableIncome, d => d.Ignore());
        }
    }
}
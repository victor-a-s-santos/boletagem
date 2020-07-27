using AutoMapper;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace src.Areas.Tickets.Mappings
{
    public class TicketCurrencyTermMapping : Profile
    {
        public TicketCurrencyTermMapping()
        {
            CreateMap<TicketCurrencyTerm, TicketCurrencyTermModel>()
                .ForMember(d => d.Id, o => o.MapFrom(t => t.Ticket.Id))
                .ForMember(d => d.OperationDate, o => o.MapFrom(t => t.Ticket.OperationDate))
                //.ForMember(d => d.WorkflowStartDate, o => o.MapFrom(t => t.Ticket.WorkflowStartDate))
                //.ForMember(d => d.WorkflowEndDate, o => o.MapFrom(t => t.Ticket.WorkflowEndDate))
                .ForMember(d => d.AccountManagerName, o => o.MapFrom(t => t.Ticket.AccountManagerName))
                .ForMember(d => d.AccountManagerDocument, o => o.MapFrom(t => t.Ticket.AccountManagerDocument))
                .ForMember(d => d.Annotations, o => o.MapFrom(t => t.Ticket.Annotations))
                .ForPath(d => d.Portfolio, o => o.MapFrom(t => t.Ticket.Portfolio))
                .ForPath(d => d.Counterpart, o => o.MapFrom(t => t.Counterpart))
                .ReverseMap()
                .ForPath(o => o.Ticket.Id, t => t.Ignore())
                .ForPath(o => o.Ticket.OperationDate, t => t.Ignore());
        }
    }
}
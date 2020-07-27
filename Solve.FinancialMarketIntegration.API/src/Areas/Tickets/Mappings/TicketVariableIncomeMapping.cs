using AutoMapper;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace src.Areas.Tickets.Mappings
{
    public class TicketVariableIncomeMapping : Profile
    {
        public TicketVariableIncomeMapping()
        {
            CreateMap<TicketVariableIncome, TicketVariableIncomeModel>()
                .ForMember(d => d.Id, o => o.MapFrom(t => t.Ticket.Id))
                .ForMember(d => d.OperationDate, o => o.MapFrom(t => t.Ticket.OperationDate))
                //.ForMember(d => d.WorkflowStartDate, o => o.MapFrom(t => t.Ticket.WorkflowStartDate))
                //.ForMember(d => d.WorkflowEndDate, o => o.MapFrom(t => t.Ticket.WorkflowEndDate))
                .ForMember(d => d.AccountManagerName, o => o.MapFrom(t => t.Ticket.AccountManagerName))
                .ForMember(d => d.AccountManagerDocument, o => o.MapFrom(t => t.Ticket.AccountManagerDocument))
                .ForMember(d => d.Items, o => o.MapFrom(t => t.LineItems))
                .ReverseMap()
                .ForPath(o => o.Ticket.Id, t => t.Ignore())
                .ForPath(o => o.Ticket.OperationDate, t => t.Ignore());
        }
    }
}
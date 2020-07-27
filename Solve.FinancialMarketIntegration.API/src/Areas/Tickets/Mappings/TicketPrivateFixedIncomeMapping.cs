using AutoMapper;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace src.Areas.Tickets.Mappings
{
    public class TicketPrivateFixedIncomeMapping : Profile
    {
        public TicketPrivateFixedIncomeMapping()
        {
            CreateMap<TicketPrivateFixedIncome, TicketPrivateFixedIncomeModel>()
                .ForMember(d => d.Id, o => o.MapFrom(t => t.Ticket.Id))
                .ForMember(d => d.OperationDate, o => o.MapFrom(t => t.Ticket.OperationDate))
                //.ForMember(d => d.WorkflowStartDate, o => o.MapFrom(t => t.Ticket.WorkflowStartDate))
                //.ForMember(d => d.WorkflowEndDate, o => o.MapFrom(t => t.Ticket.WorkflowEndDate))
                .ForMember(d => d.AccountManagerName, o => o.MapFrom(t => t.Ticket.AccountManagerName))
                .ForMember(d => d.AccountManagerDocument, o => o.MapFrom(t => t.Ticket.AccountManagerDocument))
                .ForMember(d => d.Annotations, o => o.MapFrom(t => t.Ticket.Annotations))
                .ForMember(d => d.PortfolioBank, o => o.MapFrom(t => t.Ticket.Portfolio.Bank))
                .ForMember(d => d.PortfolioBranch, o => o.MapFrom(t => t.Ticket.Portfolio.Branch))
                .ForMember(d => d.PortfolioAccount, o => o.MapFrom(t => t.Ticket.Portfolio.Account))
                .ForMember(d => d.PortfolioCetipAccount, o => o.MapFrom(t => t.Ticket.Portfolio.ClearingAccount))
                .ForMember(d => d.PortfolioCode, o => o.MapFrom(t => t.Ticket.Portfolio.Code))
                .ForMember(d => d.PortfolioDocument, o => o.MapFrom(t => t.Ticket.Portfolio.Document))
                .ForMember(d => d.PortfolioName, o => o.MapFrom(t => t.Ticket.Portfolio.Name))
                .ForMember(d => d.CounterpartCetipAccount, o => o.MapFrom(t => t.Counterpart.ClearingAccount))
                .ForMember(d => d.Command, o => o.MapFrom(t => t.Counterpart.Command))
                .ReverseMap()
                .ForPath(o => o.Ticket.Id, t => t.Ignore())
                .ForPath(o => o.Ticket.OperationDate, t => t.Ignore());
        }
    }
}
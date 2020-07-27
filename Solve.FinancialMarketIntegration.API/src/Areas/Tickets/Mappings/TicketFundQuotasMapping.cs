using AutoMapper;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace src.Areas.Tickets.Mappings
{
    public class TicketFundQuotasMapping : Profile
    {
        public TicketFundQuotasMapping()
        {
            CreateMap<TicketFundQuotas, TicketFundQuotaModel>()
                .ForMember(d => d.Id, o => o.MapFrom(t => t.Ticket.Id))
                .ForMember(d => d.OperationDate, o => o.MapFrom(t => t.Ticket.OperationDate))
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
                .ForMember(d => d.IsCetipVoice, o => o.MapFrom(t => t.IsCetipVoice))
                .ForMember(d => d.CetipVoiceId, o => o.MapFrom(t => t.CetipVoiceId))
                .ForMember(d => d.IssuerName, o => o.MapFrom(t => t.Fund.IssuerName))
                .ForMember(d => d.IsNewFund, o => o.MapFrom(t => t.Fund.IsNewFund))
                .ForMember(d => d.MnemonicCode, o => o.MapFrom(t => t.Fund.MnemonicCode))
                .ForMember(d => d.IsFIDC, o => o.MapFrom(t => t.Fund.IsFIDC))
                .ReverseMap()
                .ForPath(o => o.Ticket.Id, t => t.Ignore())
                .ForPath(o => o.Ticket.OperationDate, t => t.Ignore());
        }
    }
}
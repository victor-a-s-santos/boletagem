using FluentValidation;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Enums;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Validators
{
    public class TicketFundQuotaSchemaValidator : AbstractValidator<TicketFundQuotaModel>
    {
        public TicketFundQuotaSchemaValidator()
        {
            RuleFor(t => t.PortfolioDocument).NotEmpty();
            RuleFor(t => t.PortfolioAccount).NotEmpty();
            RuleFor(t => t.PortfolioName).NotEmpty();

            RuleFor(t => t.OperationTypeId).NotEmpty();
            RuleFor(t => t.IsSecondaryMarket).NotNull();
            RuleFor(t => t.IsIssueUnitPrice).NotNull();
            RuleFor(t => t.SettlementTypeId).NotEmpty();
            RuleFor(t => t.OperationValue).NotEmpty();
            RuleFor(t => t.FundName).NotEmpty().MaximumLength(90);
            RuleFor(t => t.FundDocument).NotEmpty().MaximumLength(14).IsCNPJ();
            RuleFor(t => t.IsFIDC).NotNull();
            RuleFor(t => t.IsNewFund).NotNull();
            RuleFor(t => t.FundType).NotEmpty().MaximumLength(3);
            RuleFor(t => t.PortfolioBank).MaximumLength(20);
            RuleFor(t => t.PortfolioBranch).MaximumLength(10);
            RuleFor(t => t.PortfolioAccount).NotEmpty().MaximumLength(15);
            RuleFor(t => t.QuotationDate).NotNull();

            When(t => t.IsFIDC, () =>
            {
                RuleFor(t => t.FundClassSeries)
                    .NotEmpty()
                    .MaximumLength(12);
            });

            When(t => t.IsNewFund, () =>
            {
                RuleFor(t => t.IssuerName)
                    .NotEmpty()
                    .MaximumLength(90);
            });

            When(t => t.IsSecondaryMarket && t.SettlementTypeId == (int)SettlementTypes.CETIP && t.FundType == "CFF", () =>
            {
                RuleFor(t => t.IsCetipVoice).NotNull();
            });

            When(t => t.IsCetipVoice.HasValue && t.IsCetipVoice.Value, () =>
            {
                RuleFor(t => t.CetipVoiceId)
                    .NotEmpty()
                    .MaximumLength(20);
            });

            When(t => (t.IsSecondaryMarket && !t.IsIssueUnitPrice) || (!t.IsSecondaryMarket && t.IsIssueUnitPrice), () =>
            {
                RuleFor(t => t.Amount).NotEmpty();
                RuleFor(t => t.QuotaValue).NotEmpty();
            });

            When(t => t.SettlementTypeId == (int)SettlementTypes.CETIP, () =>
            {
                RuleFor(t => t.PortfolioCetipAccount).NotEmpty().MaximumLength(10);
                RuleFor(t => t.CounterpartCetipAccount).NotEmpty().MaximumLength(10);
                RuleFor(t => t.MnemonicCode).NotEmpty().MaximumLength(20);
                RuleFor(t => t.Command).MaximumLength(10);
            });

            When(t => t.SettlementTypeId == (int)SettlementTypes.TED, () =>
            {
                RuleFor(t => t.FundBank).NotEmpty().MaximumLength(20);
                RuleFor(t => t.FundBranch).NotEmpty().MaximumLength(10);
                RuleFor(t => t.FundAccount).NotEmpty().MaximumLength(15);
                RuleFor(t => t.HasSameOwnership).NotNull();

            });

            When(t => t.SettlementTypeId == (int)SettlementTypes.CETIP ||
                     (t.SettlementTypeId == (int)SettlementTypes.TED && !t.HasSameOwnership.Value), () =>
                     {
                         RuleFor(t => t.CounterpartDocument).NotEmpty().MaximumLength(14).IsCNPJ();
                         RuleFor(t => t.CounterpartName).NotEmpty().MaximumLength(90);
                     });

            When(t => t.OperationTypeId == (int)TipoOperacaoEnum.Venda, () => {
                RuleFor(t => t.SettlementDate).NotNull();
            });
        }
    }
}

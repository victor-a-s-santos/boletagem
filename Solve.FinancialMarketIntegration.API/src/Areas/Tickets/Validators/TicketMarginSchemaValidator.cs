using FluentValidation;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Validators
{
    public class TicketMarginSchemaValidator : AbstractValidator<TicketMarginModel>
    {
        public TicketMarginSchemaValidator() {
            RuleFor(t => t.OperationTypeId).NotEmpty();
            RuleFor(t => t.MarketTypeId).NotEmpty();

            RuleFor(t => t.CounterpartDocument).NotEmpty().MaximumLength(14).IsCNPJ();
            RuleFor(t => t.CounterpartName).NotEmpty().MaximumLength(90);
            
            RuleFor(t => t.PortfolioBank).MaximumLength(20);
            RuleFor(t => t.PortfolioBranch).MaximumLength(10);
            RuleFor(t => t.PortfolioAccount).NotEmpty().MaximumLength(15);

            When(t => t.MarketTypeId != (int)MarketTypes.Dinheiro, () => {
                RuleFor(t => t.PortfolioClearingAccount).NotEmpty().MaximumLength(10);
                RuleFor(t => t.CounterpartClearingAccount).NotEmpty().MaximumLength(10);
                RuleFor(t => t.Command).NotNull().MaximumLength(10);
            });

            When(t => t.MarketTypeId == (int)MarketTypes.RFTituloPrivado, () => {
                RuleFor(t => t.Amount).NotNull();
                RuleFor(t => t.UnitPrice).NotNull();
                RuleFor(t => t.OperationValue).NotNull();
                RuleFor(t => t.SecurityType).NotEmpty();
                RuleFor(t => t.SecurityCode).NotEmpty();
                RuleFor(t => t.DueDate).NotEmpty();
            });

            When(t => t.MarketTypeId == (int)MarketTypes.RFTituloPublico, () => {
                RuleFor(t => t.Amount).NotNull();
                RuleFor(t => t.UnitPrice).NotNull();
                RuleFor(t => t.OperationValue).NotNull();
                RuleFor(t => t.SecurityName).NotEmpty().MaximumLength(90);
                RuleFor(t => t.SecurityCode).NotEmpty();
                RuleFor(t => t.DueDate).NotEmpty();
            });

            When(t => t.MarketTypeId == (int)MarketTypes.RendaVariavel, () => {
                RuleFor(t => t.Amount).NotEmpty();
                RuleFor(t => t.Quotation).NotEmpty();
                RuleFor(t => t.Asset).NotEmpty().MaximumLength(90);
                RuleFor(t => t.CounterpartBrokerAccount).NotEmpty().MaximumLength(10);
            });

            When(t => t.MarketTypeId == (int)MarketTypes.Dinheiro, () => {
                RuleFor(t => t.OperationValue).NotEmpty();
                RuleFor(t => t.Bank).NotEmpty().MaximumLength(20);
                RuleFor(t => t.Branch).NotEmpty().MaximumLength(10);
                RuleFor(t => t.Account).NotEmpty().MaximumLength(15);
            });
        }
    }
}
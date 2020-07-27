using FluentValidation;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Validators
{
    public class TicketSwapCetipSchemaValidator : AbstractValidator<TicketSwapCetipModel>
    {
        public TicketSwapCetipSchemaValidator() {
            RuleFor(t => t.ActiveIndex).NotEmpty().MaximumLength(10);
            RuleFor(t => t.ActiveIndexBase).NotEmpty();
            RuleFor(t => t.ActiveIndexPercent).NotNull();
            RuleFor(t => t.ActiveIndexTax).NotNull();
            RuleFor(t => t.ActiveInterestType).NotEmpty();

            RuleFor(t => t.CounterpartCetipAccount).NotEmpty().MaximumLength(15);
            RuleFor(t => t.CounterpartDocument).NotEmpty().MaximumLength(14).IsCNPJ();
            RuleFor(t => t.CounterpartName).NotEmpty().MaximumLength(90);
            RuleFor(t => t.ExpirationDate).NotEmpty();
            RuleFor(t => t.OperationValue).NotEmpty();

            RuleFor(t => t.PassiveIndex).NotNull().MaximumLength(10);
            RuleFor(t => t.PassiveIndexBase).NotEmpty();
            RuleFor(t => t.PassiveIndexPercent).NotNull();
            RuleFor(t => t.PassiveIndexTax).NotNull();
            RuleFor(t => t.PassiveInterestType).NotEmpty();

            RuleFor(t => t.PortfolioBank).MaximumLength(20);
            RuleFor(t => t.PortfolioBranch).MaximumLength(10);
            RuleFor(t => t.PortfolioAccount).NotEmpty().MaximumLength(15);
            RuleFor(t => t.PortfolioCetipAccount).NotEmpty().MaximumLength(15);
            RuleFor(t => t.PortfolioDocument).NotEmpty().MaximumLength(14).IsCNPJ();
            RuleFor(t => t.PortfolioName).NotEmpty().MaximumLength(90);

            RuleFor(t => t.Command).MaximumLength(10);
            RuleFor(t => t.Annotations).MaximumLength(200);
        }
    }
}

using FluentValidation;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Validators
{
    public class TicketCurrencyTermSchemaValidator : AbstractValidator<TicketCurrencyTermModel>
    {
        public TicketCurrencyTermSchemaValidator() {
            RuleFor(t => t.CetipSettlement).NotNull();
            RuleFor(t => t.CrossRate).NotNull();
            RuleFor(t => t.CurrencyId).NotEmpty();
            RuleFor(t => t.ExpirationDate).NotEmpty();
            RuleFor(t => t.FutureFee).NotNull();
            RuleFor(t => t.OperationValue).NotEmpty();
            RuleFor(t => t.QuoteExpirationTypeId).NotEmpty();
            RuleFor(t => t.Portfolio.Bank).MaximumLength(20);
            RuleFor(t => t.Portfolio.Branch).MaximumLength(10);
            RuleFor(t => t.Portfolio.Account).NotEmpty().MaximumLength(15);

            RuleFor(t => t.ContractNumber).MaximumLength(15); //MaxLength 100 on DB

            When(t => t.Counterpart != null, () => {
                RuleFor(t => t.Counterpart.ClearingAccount).MaximumLength(8); // 10 on DB
                RuleFor(t => t.Counterpart.Command).MaximumLength(10);
                RuleFor(t => t.Counterpart.Document).NotNull().MaximumLength(14).IsCNPJ();
                RuleFor(t => t.Counterpart.Name).MaximumLength(90);
            });
        }
    }
}

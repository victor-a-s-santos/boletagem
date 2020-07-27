using FluentValidation;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Enums;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Validators
{
    public class TicketPrivateFixedIncomeSchemaValidator : AbstractValidator<TicketPrivateFixedIncomeModel>
    {
        public TicketPrivateFixedIncomeSchemaValidator() {
            RuleFor(t => t.OperationTypeId).NotEmpty();
            RuleFor(t => t.Amount).NotEmpty();
            RuleFor(t => t.UnitPrice).NotEmpty();
            RuleFor(t => t.IsSecondaryMarket).NotNull();
            RuleFor(t => t.IsTerm).NotNull();
            RuleFor(t => t.OperationValue).NotEmpty();
            RuleFor(t => t.ExpirationDate).NotEmpty();
            RuleFor(t => t.IssueFee).NotNull();
            RuleFor(t => t.IssueDate).NotEmpty();

            RuleFor(t => t.AssetType).MaximumLength(20);
            RuleFor(t => t.AssetCode).MaximumLength(15);
            RuleFor(t => t.CounterpartDocument).NotEmpty().MaximumLength(14).IsCNPJ();
            RuleFor(t => t.CounterpartName).MaximumLength(90);
            RuleFor(t => t.CounterpartCetipAccount).MaximumLength(10);
            RuleFor(t => t.Command).MaximumLength(10);
            RuleFor(t => t.PortfolioBank).MaximumLength(20);
            RuleFor(t => t.PortfolioBranch).MaximumLength(10);
            RuleFor(t => t.PortfolioAccount).NotEmpty().MaximumLength(15);
        }
    }
}

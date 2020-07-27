using FluentValidation;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Enums;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Validators
{
    public class TicketPublicFixedIncomeSchemaValidator : AbstractValidator<TicketPublicFixedIncomeModel>
    {
        public TicketPublicFixedIncomeSchemaValidator() {
            RuleFor(t => t.OperationTypeId).NotEmpty();
            RuleFor(t => t.OperationValue).NotEmpty();
            RuleFor(t => t.UnitPrice).NotEmpty();
            RuleFor(t => t.Amount).NotEmpty();

            RuleFor(t => t.Security).NotEmpty().MaximumLength(80);
            RuleFor(t => t.SecurityId).NotEmpty().MaximumLength(50);
            RuleFor(t => t.CounterpartDocument).NotEmpty().MaximumLength(14).IsCNPJ();
            RuleFor(t => t.CounterpartName).NotEmpty().MaximumLength(90);
            RuleFor(t => t.CounterpartSelicAccount).NotEmpty().MaximumLength(10);
        
            RuleFor(t => t.Command).MaximumLength(10);

            RuleFor(t => t.PortfolioBank).MaximumLength(20);
            RuleFor(t => t.PortfolioBranch).MaximumLength(10);
            RuleFor(t => t.PortfolioAccount).NotEmpty().MaximumLength(15);
            RuleFor(t => t.PortfolioDocument).NotEmpty().MaximumLength(14).IsCNPJ();
            RuleFor(t => t.PortfolioName).NotEmpty().MaximumLength(90);
            RuleFor(t => t.PortfolioSelicAccount).NotEmpty().MaximumLength(15);

            When(t => t.SettlementTypeId == (int)SettlementTypes.Term, () => {
                RuleFor(t => t.SettlementDate).NotEmpty();
            });
        }
    }
}

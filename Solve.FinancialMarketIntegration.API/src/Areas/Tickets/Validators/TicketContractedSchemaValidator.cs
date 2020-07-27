using FluentValidation;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Validators
{
    public class TicketContractedSchemaValidator : AbstractValidator<TicketContractedModel>
    {
        public TicketContractedSchemaValidator() {
            RuleFor(t => t.Amount).NotEmpty();
            RuleFor(t => t.Command).NotEmpty().MaximumLength(10);

            RuleFor(t => t.CounterpartSelicAccount).NotEmpty().MaximumLength(15);      
            RuleFor(t => t.CounterpartDocument).NotEmpty().MaximumLength(14).IsCNPJ();      
            RuleFor(t => t.CounterpartName).NotEmpty().MaximumLength(90);

            RuleFor(t => t.Security).NotEmpty().MaximumLength(80);
            RuleFor(t => t.SecurityId).NotEmpty().MaximumLength(50);

            RuleFor(t => t.PortfolioDocument).NotEmpty().MaximumLength(14).IsCNPJ();
            RuleFor(t => t.PortfolioBank).MaximumLength(20);
            RuleFor(t => t.PortfolioBranch).MaximumLength(10);
            RuleFor(t => t.PortfolioAccount).NotEmpty().MaximumLength(15);
            RuleFor(t => t.PortfolioName).NotEmpty().MaximumLength(90);
            RuleFor(t => t.PortfolioSelicAccount).NotEmpty().MaximumLength(15);

            RuleFor(t => t.UnitPriceOutward).NotEmpty();
            RuleFor(t => t.UnitPriceReturn).NotEmpty();
            RuleFor(t => t.ReturnDate).NotEmpty();
            RuleFor(t => t.ValueOutward).NotEmpty();
            RuleFor(t => t.ValueReturn).NotEmpty();
            RuleFor(t => t.IssueTax).NotEmpty();
            RuleFor(t => t.ExpirationDate).NotEmpty();

            RuleFor(t => t.Command).NotEmpty();
        }
    }
}

using FluentValidation;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Validators
{
    public class TicketFutureSchemaValidator : AbstractValidator<TicketFuturesModel> {
        public TicketFutureSchemaValidator() {
            RuleFor(t => t.OperationTypeId).NotEmpty();
            RuleFor(t => t.Amount).NotEmpty();
            RuleFor(t => t.UnitPrice).NotEmpty();
            RuleFor(t => t.TradingDate).NotEmpty();
            RuleFor(t => t.PercentageDiscount).NotEmpty();
            RuleFor(t => t.PaperCode).NotEmpty();
            RuleFor(t => t.PaperSerie).NotEmpty().MaximumLength(10);
            RuleFor(t => t.Broker).NotEmpty();
            RuleFor(t => t.BrokerAccount).NotEmpty();
            RuleFor(t => t.BrokerCode).NotEmpty().MaximumLength(15);
            RuleFor(t => t.BrokerDocument).NotEmpty().MaximumLength(14).IsCNPJ();
            RuleFor(t => t.PortfolioBank).MaximumLength(20);
            RuleFor(t => t.PortfolioBranch).MaximumLength(10);
            RuleFor(t => t.PortfolioAccount).NotEmpty().MaximumLength(15);
            RuleFor(t => t.PortfolioB3Account).NotEmpty().MaximumLength(15);
            RuleFor(t => t.PortfolioDocument).NotEmpty().MaximumLength(14).IsCNPJ();
            RuleFor(t => t.PortfolioName).NotEmpty().MaximumLength(90);

        }
    }
}

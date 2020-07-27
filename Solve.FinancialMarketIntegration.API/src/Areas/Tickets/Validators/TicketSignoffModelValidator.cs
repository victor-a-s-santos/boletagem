using System;
using FluentValidation;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Validators
{
    public class TicketSignoffValidator : AbstractValidator<TicketSignoffModel> {
        public TicketSignoffValidator() {
            RuleFor(t => t.TimeLimit).NotEmpty();
        }
    }
}


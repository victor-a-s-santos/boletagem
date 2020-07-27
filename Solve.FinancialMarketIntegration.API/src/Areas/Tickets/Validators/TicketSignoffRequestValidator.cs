using System;
using FluentValidation;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Validators
{
    public class TicketSignoffRequestValidator : AbstractValidator<TicketSignoffRequestModel>
    {
        public TicketSignoffRequestValidator()
        {
            RuleFor(t => t.TimeLimit).NotNull()
                .GreaterThan(DateTimeOffset.UtcNow.TimeOfDay)
                .WithMessage(t => "Horário limite (" + t.TimeLimit + ") deve ser maior que horário atual " + DateTimeOffset.UtcNow.TimeOfDay.ToString("hh\\:mm"));

            RuleFor(t => t.Justificative).NotEmpty()
                .MinimumLength(10)
                .MaximumLength(280);
        }
    }
}


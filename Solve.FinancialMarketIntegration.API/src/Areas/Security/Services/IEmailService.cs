using Solve.FinancialMarketIntegration.API.Areas.Security.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string emailTo,
            string subject,
            string message = null,
            EmailTemplateEnum? templateEnum = null,
            Dictionary<string, string> valoresReplace = null);
    }
}

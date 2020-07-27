using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Entities.Enums
{
    public enum EmailTemplateEnum
    {
        [Description("password-forgot.html")]
        PasswordForgot = 1,
        [Description("password-creation.html")]
        PasswordCreation = 2
    }
}

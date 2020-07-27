using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Models
{
    public class UserPasswordRecovery
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string PasswordResetHash { get; set; }
    }
}

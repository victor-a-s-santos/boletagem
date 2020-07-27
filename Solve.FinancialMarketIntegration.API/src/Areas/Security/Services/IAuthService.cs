using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Security.Models;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Services
{
    public interface IAuthService
    {
        UserAuthentication PasswordReset(string username, string newPassword, string oldPassword = null, string passwordResetHash = null);
        UserAuthentication Authenticate(string username, string password, bool ignoreExpiredPassword = false);
        bool HasRoles(string username, IEnumerable<int> roles);
        IEnumerable<UserData> GetUserNames(IEnumerable<string> usernames);
        IEnumerable<Role> GetRoles(string username);
        IEnumerable<Role> GetRoles(ClaimsPrincipal user);
        Task PasswordForgot(string username);
    }

    public class UserData {
        public string User { get; set; }
        public string Name { get; set; }
    }
}
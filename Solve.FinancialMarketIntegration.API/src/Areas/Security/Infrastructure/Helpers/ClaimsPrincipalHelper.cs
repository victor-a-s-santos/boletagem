using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Security.Models;
using Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.Helpers
{
    public static class ClaimsPrincipalHelper
    {
        public static string GetUsername(this ClaimsPrincipal identity)
        {
            return identity.FindFirstValue(ClaimTypes.Name);
        }
    }
}

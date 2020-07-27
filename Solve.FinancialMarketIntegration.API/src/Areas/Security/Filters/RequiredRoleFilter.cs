using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Security.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Filters
{
    public class RequiredRoleFilter : IActionFilter
    {
        private int[] roles;
        private IAuthService service;

        public RequiredRoleFilter(int[] value, IAuthService service)
        {
            this.roles = value;
            this.service = service;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var username = context.HttpContext.User.Identity.Name;

            if (service.HasRoles(username, roles))
                return;

            context.Result = new UnauthorizedObjectResult("User not authorize to access this resource. Roles " + String.Join(", ", roles)  + " are required");
        }
    }
}

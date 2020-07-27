using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Filters
{
    public class CheckTokenFilter : IActionFilter
    {
        private DataContext context;

        public CheckTokenFilter(DataContext context)
        {
            this.context = context;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var username = context.HttpContext.User.Identity.Name;
            var authorization = context.HttpContext.Request.Headers["Authorization"];

            if (authorization.Count == 0)
                return;

            var authorizationBearer = authorization[0];
            var token = authorizationBearer.Substring("Bearer ".Length).Trim();

            if (!this.context.Users.Any(u => u.UserName == username && u.LastAccessToken == token))
            {
                context.Result = new UnauthorizedObjectResult(new { Message = "session-expired" });
                return;
            }
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Filters
{
    public class RequiredAccountManagerFilter : ActionFilterAttribute
    {
        private IAccountManagerService service;

        public RequiredAccountManagerFilter(IAccountManagerService service)
        {
            if ((this.service = service) == null)
            throw new ArgumentNullException(paramName: "service");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!service.IsAccountManager(context.HttpContext.User.Identity.Name))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
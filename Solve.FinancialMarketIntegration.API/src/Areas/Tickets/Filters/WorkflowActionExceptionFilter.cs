using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Exceptions;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Filters
{
    public class WorkflowActionExceptionFilter : ExceptionFilterAttribute
    {
        public WorkflowActionExceptionFilter()
        {
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is InvalidOperationException || context.Exception is ForbiddenException)
            {
                context.Result = new ForbidResult();
                return;
            }

            if (context.Exception is NotFoundException){
                context.Result = new NotFoundResult();
                return;
            }
        }
    }
}
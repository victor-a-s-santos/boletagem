using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Exceptions;
using AutoMapper;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Filters;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Controllers
{
    [Route("api/v1/tickets/hc")]
    [ApiController]
    [TicketActionExceptionFilter]
    public class HCController : ControllerBase
    {
        private static string RANDOM_STRING = "1231280391280938129038921";

        [HttpGet("hello/{input}")]
        public ActionResult<string> Hello([FromRoute]string input)
        {
            return new OkObjectResult(string.Format("Hello {0}", input));
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return new OkObjectResult(RANDOM_STRING);
        }
    }
}
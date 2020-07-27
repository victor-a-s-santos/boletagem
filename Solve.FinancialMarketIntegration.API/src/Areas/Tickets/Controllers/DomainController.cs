using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solve.FinancialMarketIntegration.API.Areas.Security.Services;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Controllers
{
    [Authorize]
    [Route("api/v1/tickets/[controller]")]
    [ApiController]
    public class DomainController : ControllerBase
    {
        private TicketDataContext Context { get; set; }
        private IAuthService AuthService { get; set; }

        public DomainController
        (
            TicketDataContext context,
            IAuthService authService
        )
        {
            this.Context = context;
            this.AuthService = authService;
        }

        [HttpGet]
        [Route("banks")]
        public ActionResult<IEnumerable<Bank>> GetBanks()
        {
            return Context.Bank.ToArray();
        }

        [HttpGet]
        [Route("types")]
        public ActionResult<IEnumerable<TicketType>> GetTypes()
        {
            return Context.TicketTypes.ToArray();
        }

        [HttpGet]
        [Route("operation-types")]
        public ActionResult<IEnumerable<OperationType>> GetOperationTypes()
        {
            return Context.OperationTypes.ToArray();
        }

        [HttpGet]
        [Route("settlement-types")]
        public ActionResult<IEnumerable<SettlementType>> GetSettlementTypes()
        {
            return Context.SettlementTypes.ToArray();
        }

        [HttpGet]
        [Route("market-types")]
        public ActionResult<IEnumerable<MarketType>> GetMarketTypes()
        {
            return Context.MarketTypes.ToArray();
        }
    }
}
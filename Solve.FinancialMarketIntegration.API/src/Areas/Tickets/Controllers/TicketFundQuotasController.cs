using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using Newtonsoft.Json;
using Solve.FinancialMarketIntegration.API.Areas.Security.Services;
using Solve.FinancialMarketIntegration.API.Areas.Security.Filters;
using AutoMapper;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Filters;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Events;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Events.DomainEvents;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Controllers
{
    [Authorize]
    [Route("api/v1/tickets/quota")]
    [ApiController]
    [TicketActionExceptionFilter]
    public class TicketFundQuotasController : ControllerBase, ITicketService<TicketFundQuotaModel>
    {
        private TicketDataContext context;
        private IWorkflowService workflowService;
        private IAccountManagerService accountManagerService;
        private IMapper mapper;

        private TicketListQueryService<TicketFundQuotas, TicketFundQuotaModel> ticketQueryPaginatedService;

        public TicketFundQuotasController(
            TicketDataContext context,
            IWorkflowService workflowService,
            IAccountManagerService accountManagerService,
            IMapper mapper,
            TicketListQueryService<TicketFundQuotas, TicketFundQuotaModel> ticketQueryPaginatedService
        )
        {
            this.context = context;
            this.workflowService = workflowService;
            this.accountManagerService = accountManagerService;
            this.mapper = mapper;
            this.ticketQueryPaginatedService = ticketQueryPaginatedService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketFundQuotaModel>> Get([FromRoute]int id)
        {
            var result = await GetByIdAsync(User.Identity.Name, id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketFundQuotaModel>>> Get([FromQuery] TicketFilter filter)
        {
            var result = await ListAsync(User.Identity.Name, filter);
            return Ok(result);
        }

        [HttpGet]
        [Route("list-paginated")]
        public async Task<ActionResult<PagedResult<TicketFundQuotaModel>>> ListPaginated([FromQuery] TicketFilter filter)
        {
            var result = await ListPaginatedAsync(User.Identity.Name, filter);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(RequiredAccountManagerFilter))]
        public async Task<ActionResult<TicketFundQuotaModel>> Put([FromRoute]int id, [FromBody]TicketFundQuotaModel body)
        {
            var result = await EditAsync(User.Identity.Name, id, body);
            return Ok(result);
        }

        [HttpPost]
        [TypeFilter(typeof(RequiredRoleFilter), Arguments = new object[] { new[] { Roles.CreateQuotaTicket } })]
        [TypeFilter(typeof(RequiredAccountManagerFilter))]
        public async Task<ActionResult<TicketFundQuotaModel>> Post([FromServices]EventBus eventBus, [FromBody]TicketFundQuotaModel body)
        {
            var result = await RegisterNewAsync(User.Identity.Name, body);
            eventBus.Dispatch(new TicketRegisteredEvent(ticketId: result.Id.Value, user: User.Identity.Name));
            
            return Ok(result);
        }

        [HttpPost("{id}/forward")]
        [TypeFilter(typeof(RequiredAccountManagerFilter))]
        public async Task<ActionResult<string>> Forward([FromServices]EventBus eventBus, [FromRoute]int id)
        {
            var newStatus = await ForwardAsync(User.Identity.Name, id);
            eventBus.Dispatch(new TicketForwardedEvent(ticketId: id, user: User.Identity.Name));
            
            return Ok(new { statusDescription = newStatus });
        }

        [HttpPost("{id}/cancel")]
        [TypeFilter(typeof(RequiredAccountManagerFilter))]
        public async Task<ActionResult<string>> CancelDraft([FromServices]EventBus eventBus, [FromRoute]int id)
        {
            var newStatus = await CancelDraftAsync(User.Identity.Name, id);
            eventBus.Dispatch(new TicketCancelledEvent(ticketId: id, user: User.Identity.Name));

            return Ok(new { statusDescription = newStatus });
        }

        // Implementations

        [NonAction]
        public async Task<TicketFundQuotaModel> GetByIdAsync(string username, int ticketId)
        {
            var query = context.TicketFundQuotas
                .Include(t => t.Ticket)
                .Include(t => t.OperationType)
                .Include(t => t.SettlementType)
                .Where(t => t.Ticket.Id == ticketId);


            var accountManager = accountManagerService.GetAccountManager(username);
            if (accountManager != null)
            {
                query = query.Where(t => t.Ticket.AccountManager.Id == accountManager.Id);
            }

            var data = await query.SingleOrDefaultAsync();
            if (data == null)
            {
                throw new Exception("not-found");
            }

            var result = mapper.Map<TicketFundQuotaModel>(data);

            if (data.Ticket.WorkflowId != null)
            {
               result.MapWorkflowData(await workflowService.GetWorkflowDetailsAsync(data.Ticket.WorkflowId.Value));
            }

            return result;
        }

        [NonAction]
        public async Task<IEnumerable<TicketFundQuotaModel>> ListAsync(string username, TicketFilter filter)
        {
            var accountManager = accountManagerService.GetAccountManager(username);
            var query = context.TicketFundQuotas
                .AsNoTracking()
                .ApplyFilter(filter, accountManager)
                .Include(t => t.OperationType)
                .Include(t => t.SettlementType)
                .Include(t => t.Ticket);

            var data = await query.ToListAsync();

            if (data.Count == 0)
            {
                return new TicketFundQuotaModel[] { };
            }

            var ids = data
                .Where(d => d.Ticket.WorkflowId != null)
                .Select(d => (int)d.Ticket.WorkflowId);

            var workflowDetails = (await workflowService.GetWorkflowsDetailAsync(ids)).ToDictionary(kvp => kvp.WorkflowId, kvp => kvp);

            var result = data
                .Select(ticket =>
                {
                    var mapped = mapper.Map<TicketFundQuotaModel>(ticket);

                    mapped.MapWorkflowData(workflowDetails, ticket.Ticket.WorkflowId);

                    return mapped;
                })
                .FilterWorkflowStatus(filter.StatusId);

            return result.ToList();
        }

        [NonAction]
        public async Task<PagedResult<TicketFundQuotaModel>> ListPaginatedAsync(string username, TicketFilter filter)
        {
            var query = context.TicketFundQuotas
                .Include(t => t.OperationType)
                .Include(t => t.SettlementType)
                .Include(t => t.Ticket);

            var result = await ticketQueryPaginatedService.By(query)
                                                          .Using(filter)
                                                          .ListPaginatedAsync(username);
            return result;
        }

        [NonAction]
        public async Task<TicketFundQuotaModel> EditAsync(string username, int ticketId, TicketFundQuotaModel ticketData)
        {
            var accountManager = accountManagerService.GetAccountManager(username);

            var ticket = await context.TicketFundQuotas
                .Include(t => t.Ticket)
                .SingleOrDefaultAsync(t => t.Ticket.Id == ticketId && t.Ticket.AccountManager.Id == accountManager.Id);

            if (ticket == null)
            {
                throw new Exception("not-found");
            }

            if (ticket.Ticket.WorkflowId != null)
            {
                throw new InvalidOperationException("invalid-workflow-status");
            }

            mapper.Map(ticketData, ticket);

            context.Attach(ticket.OperationType);
            context.Attach(ticket.SettlementType);

            await context.SaveChangesAsync();

            var result = mapper.Map<TicketFundQuotaModel>(ticket);

            return await Task.FromResult(result);
        }

        [NonAction]
        public async Task<TicketFundQuotaModel> RegisterNewAsync(string username, TicketFundQuotaModel ticketData)
        {
            var accountManager = accountManagerService.GetAccountManager(username);

            var newTicket = mapper.Map<TicketFundQuotas>(ticketData);

            if (newTicket.Ticket.OperationDate == null || newTicket.Ticket.OperationDate == DateTimeOffset.MinValue)
            {
                newTicket.Ticket.OperationDate = DateTimeOffset.Now;
            }

            if (newTicket.IsSecondaryMarket && newTicket.Fund.Type.Equals("CFF") && newTicket.SettlementType.Id == (int)SettlementTypes.CETIP)
            {
                newTicket.IsCetipVoice = true;
            }
            else
            {
                newTicket.IsCetipVoice = false;
            }

            newTicket.Ticket.Type = new TicketType { Id = (short)TicketTypes.FundQuota };
            newTicket.Ticket.AccountManager = accountManager;
            newTicket.Ticket.AccountManagerName = accountManager.Name;
            newTicket.Ticket.AccountManagerDocument = accountManager.Document;

            context.Attach(newTicket.OperationType);
            context.Attach(newTicket.SettlementType);
            context.Attach(newTicket.Ticket.Type);
            context.Attach(newTicket.Ticket.AccountManager);

            context.TicketFundQuotas.Add(newTicket);
            await context.SaveChangesAsync();

            var result = mapper.Map<TicketFundQuotaModel>(newTicket);

            return await Task.FromResult(result);
        }

        [NonAction]
        public async Task<string> ForwardAsync(string username, int ticketId)
        {
            return await OpenWorkflowAsync(username, ticketId, workflowService.OpenAsync);
        }

        [NonAction]
        public async Task<string> CancelDraftAsync(string username, int ticketId)
        {
            return await OpenWorkflowAsync(username, ticketId, workflowService.CancelDraftAsync);
        }

        [NonAction]
        public async Task<string> OpenWorkflowAsync(string username, int ticketId, Func<int, string, Task<int>> wfMethod)
        {
            var current = await context.TicketFundQuotas
                .Where(t => t.Ticket.Id == ticketId)
                .Select(t => new
                {
                    WorkflowId = t.Ticket.WorkflowId,
                    OperationDate = t.Ticket.OperationDate,
                    t.IsCetipVoice,
                    SettlementTypeId = t.SettlementType.Id
                })
                .FirstOrDefaultAsync();

            if (current == null)
                throw new Exception("not-found");

            if (current.WorkflowId != null)
                throw new InvalidOperationException("invalid-workflow-status");


            var workflowType = WorkflowTypes.FundQuota;

            if (current.SettlementTypeId == (int)SettlementTypes.TED)
            {
                workflowType = WorkflowTypes.FundQuotaTED;
            }

            var workflowId = await wfMethod((int)workflowType, username);

            var ticket = new Ticket() { Id = ticketId };
            context.Ticket.Attach(ticket);

            ticket.WorkflowId = workflowId;

            var now = DateTimeOffset.Now;

            if (current.OperationDate.ToLocalTime().Day < now.Day)
            {
                ticket.OperationDate = now;
            }

            ticket.OperationDate = ticket.OperationDate.Day < now.Day ? ticket.OperationDate = now : ticket.OperationDate;

            await context.SaveChangesAsync();

            var workflowDetails = await workflowService.GetWorkflowDetailsAsync(workflowId);
            return workflowDetails.StatusDescription;
        }
    }
}

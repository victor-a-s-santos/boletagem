using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities.Enums;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Filters;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Events;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Events.DomainEvents;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Controllers
{
    [Authorize]
    [Route("api/v1/tickets/currency-term")]
    [ApiController]
    [TicketActionExceptionFilter]
    public class TicketCurrencyTermController : ControllerBase, ITicketService<TicketCurrencyTermModel>
    {
        TicketDataContext context;
        IWorkflowService workflowService;
        IAccountManagerService accountManagerService;
        IMapper mapper;

        private TicketListQueryService<TicketCurrencyTerm, TicketCurrencyTermModel> ticketQueryPaginatedService;

        public TicketCurrencyTermController(
            TicketDataContext context,
            IWorkflowService workflowService,
            IAccountManagerService accountManagerService,
            IMapper mapper,
            TicketListQueryService<TicketCurrencyTerm, TicketCurrencyTermModel> ticketQueryPaginatedService
        )
        {
            this.context = context;
            this.workflowService = workflowService;
            this.mapper = mapper;
            this.accountManagerService = accountManagerService;
            this.ticketQueryPaginatedService = ticketQueryPaginatedService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketCurrencyTermModel>> Get([FromRoute]int id)
        {
            var result = await GetByIdAsync(User.Identity.Name, id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketCurrencyTermModel>>> Get(
            [FromQuery]TicketFilter filter
        )
        {
            var result = await ListAsync(User.Identity.Name, filter);
            return Ok(result);
        }

        [HttpGet]
        [Route("list-paginated")]
        public async Task<ActionResult<PagedResult<TicketCurrencyTermModel>>> ListPaginated(
            [FromQuery]TicketFilter filter
        )
        {
            var result = await ListPaginatedAsync(
                User.Identity.Name,
                filter
            );
            return Ok(result);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(RequiredAccountManagerFilter))]
        public async Task<ActionResult<TicketCurrencyTermModel>> Put([FromRoute]int id, [FromBody]TicketCurrencyTermModel body)
        {
            var result = await EditAsync(User.Identity.Name, id, body);
            return Ok(result);
        }

        [HttpPost]
        [TypeFilter(typeof(RequiredAccountManagerFilter))]
        public async Task<ActionResult<TicketCurrencyTermModel>> Post([FromServices]EventBus eventBus, [FromBody]TicketCurrencyTermModel body)
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
        public async Task<TicketCurrencyTermModel> GetByIdAsync(string username, int ticketId)
        {
            var query = context.TicketCurrencyTerm
                .Include(t => t.Ticket)
                .Include(t => t.OperationType)
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

            var result = mapper.Map<TicketCurrencyTermModel>(data);

            if (data.Ticket.WorkflowId != null)
            {
                result.MapWorkflowData(await workflowService.GetWorkflowDetailsAsync(data.Ticket.WorkflowId.Value));
            }

            return result;
        }

        [NonAction]
        public async Task<IEnumerable<TicketCurrencyTermModel>> ListAsync(string username, TicketFilter filter)
        {
            var accountManager = accountManagerService.GetAccountManager(username);
            var query = context.TicketCurrencyTerm
                .AsNoTracking()
                .ApplyFilter(filter, accountManager)
                .Include(t => t.OperationType)
                .Include(t => t.Ticket);

            var data = await query.ToListAsync();

            if (data.Count == 0)
            {
                return new TicketCurrencyTermModel[] { };
            }

            var ids = data
                .Where(d => d.Ticket.WorkflowId != null)
                .Select(d => (int)d.Ticket.WorkflowId);

            var workflowDetails = (await workflowService.GetWorkflowsDetailAsync(ids)).ToDictionary(kvp => kvp.WorkflowId, kvp => kvp);

            var result = data
                .Select(ticket =>
                {
                    var mapped = mapper.Map<TicketCurrencyTermModel>(ticket);

                    mapped.MapWorkflowData(workflowDetails, ticket.Ticket.WorkflowId);

                    return mapped;
                })
                .FilterWorkflowStatus(filter.StatusId);

            return result.ToList();
        }

        [NonAction]
        public async Task<PagedResult<TicketCurrencyTermModel>> ListPaginatedAsync(string username, TicketFilter filter)
        {
            var query = context.TicketCurrencyTerm
                .Include(t => t.OperationType)
                .Include(t => t.Ticket);

            var result = await ticketQueryPaginatedService.By(query)
                                                          .Using(filter)
                                                          .ListPaginatedAsync(username);

            return result;
        }

        [NonAction]
        public async Task<TicketCurrencyTermModel> EditAsync(string username, int ticketId, TicketCurrencyTermModel ticketData)
        {
            var accountManager = accountManagerService.GetAccountManager(username);

            var ticket = await context.TicketCurrencyTerm
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

            await context.SaveChangesAsync();

            var result = mapper.Map<TicketCurrencyTermModel>(ticket);

            return await Task.FromResult(result);
        }

        [NonAction]
        public async Task<TicketCurrencyTermModel> RegisterNewAsync(string username, TicketCurrencyTermModel ticketData)
        {
            var accountManager = accountManagerService.GetAccountManager(username);

            var newTicket = mapper.Map<TicketCurrencyTerm>(ticketData);

            if(newTicket.Ticket.OperationDate == null || newTicket.Ticket.OperationDate == DateTimeOffset.MinValue){
                newTicket.Ticket.OperationDate = DateTimeOffset.Now;
            }

            newTicket.Ticket.Type = new TicketType { Id = (short)TicketTypes.CurrencyTerm };
            newTicket.Ticket.AccountManager = accountManager;
            newTicket.Ticket.AccountManagerName = accountManager.Name;
            newTicket.Ticket.AccountManagerDocument = accountManager.Document;

            context.Attach(newTicket.OperationType);
            context.Attach(newTicket.Ticket.Type);
            context.Attach(newTicket.Ticket.AccountManager);

            context.TicketCurrencyTerm.Add(newTicket);
            await context.SaveChangesAsync();

            var result = mapper.Map<TicketCurrencyTermModel>(newTicket);

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
            var current = await context.TicketCurrencyTerm
                .Where(t => t.Ticket.Id == ticketId)
                .Select(t => new
                {
                    WorkflowId = t.Ticket.WorkflowId,
                    OperationDate = t.Ticket.OperationDate
                })
                .FirstOrDefaultAsync();

            if (current == null)
            {
                throw new Exception("not-found");
            }

            if (current.WorkflowId != null)
            {
                throw new InvalidOperationException("invalid-workflow-status");
            }

            var workflowId = await wfMethod((int)WorkflowTypes.CurrencyTerm, username);

            var ticket = new Ticket()
            {
                Id = ticketId,
            };

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

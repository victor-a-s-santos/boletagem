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
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Filters;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Events;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Events.DomainEvents;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Controllers
{
    [Authorize]
    [Route("api/v1/tickets/private-fixed-income")]
    [ApiController]
    [TicketActionExceptionFilter]
    public class TicketPrivateFixedIncomeController : ControllerBase, ITicketService<TicketPrivateFixedIncomeModel>
    {
        private TicketDataContext context;
        private IWorkflowService workflowService;
        private IAccountManagerService accountManagerService;
        private IMapper mapper;

        private TicketListQueryService<TicketPrivateFixedIncome, TicketPrivateFixedIncomeModel> ticketQueryPaginatedService;

        public TicketPrivateFixedIncomeController(
            TicketDataContext context,
            IWorkflowService workflowService,
            IAccountManagerService accountManagerService,
            IMapper mapper,
            TicketListQueryService<TicketPrivateFixedIncome, TicketPrivateFixedIncomeModel> ticketQueryPaginatedService
        )
        {
            this.context = context;
            this.workflowService = workflowService;
            this.accountManagerService = accountManagerService;
            this.mapper = mapper;
            this.ticketQueryPaginatedService = ticketQueryPaginatedService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketPrivateFixedIncomeModel>> Get([FromRoute]int id)
        {
            var result = await GetByIdAsync(User.Identity.Name, id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketPrivateFixedIncomeModel>>> Get(
            [FromQuery]TicketFilter filter
        )
        {
            var result = await ListAsync(User.Identity.Name, filter);
            return Ok(result);
        }

        [HttpGet]
        [Route("list-paginated")]
        public async Task<ActionResult<PagedResult<TicketPrivateFixedIncomeModel>>> ListPaginated(
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
        public async Task<ActionResult<TicketPrivateFixedIncomeModel>> Put([FromRoute]int id, [FromBody]TicketPrivateFixedIncomeModel body)
        {
            var result = await EditAsync(User.Identity.Name, id, body);
            return Ok(result);
        }

        [HttpPost]
        [TypeFilter(typeof(RequiredAccountManagerFilter))]
        public async Task<ActionResult<TicketPrivateFixedIncomeModel>> Post([FromServices]EventBus eventBus, [FromBody]TicketPrivateFixedIncomeModel body)
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

        [NonAction]
        public async Task<TicketPrivateFixedIncomeModel> GetByIdAsync(string username, int ticketId)
        {
            var query = context.TicketPrivateFixedIncomes
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

            var result = mapper.Map<TicketPrivateFixedIncomeModel>(data);

            if (data.Ticket.WorkflowId != null)
            {
                result.MapWorkflowData(await workflowService.GetWorkflowDetailsAsync(data.Ticket.WorkflowId.Value));
            }

            return result;
        }

        [NonAction]
        public async Task<IEnumerable<TicketPrivateFixedIncomeModel>> ListAsync(string username, TicketFilter filter)
        {
            var accountManager = accountManagerService.GetAccountManager(username);
            var query = context.TicketPrivateFixedIncomes
                .AsNoTracking()
                .ApplyFilter(filter, accountManager)
                .Include(t => t.OperationType)
                .Include(t => t.Ticket);

            var data = await query.ToListAsync();

            if (data.Count == 0)
            {
                return await Task.FromResult(new TicketPrivateFixedIncomeModel[] { });
            }

            var ids = data
                .Where(d => d.Ticket.WorkflowId != null)
                .Select(d => (int)d.Ticket.WorkflowId);

            var workflowDetails = (await workflowService.GetWorkflowsDetailAsync(ids)).ToDictionary(kvp => kvp.WorkflowId, kvp => kvp);

            var result = data
                .Select(ticket =>
                {
                    var mapped = mapper.Map<TicketPrivateFixedIncomeModel>(ticket);

                    mapped.MapWorkflowData(workflowDetails, ticket.Ticket.WorkflowId);

                    return mapped;
                })
                .FilterWorkflowStatus(filter.StatusId);

            return result.ToList();
        }

        [NonAction]
        public async Task<PagedResult<TicketPrivateFixedIncomeModel>> ListPaginatedAsync(string username, TicketFilter filter)
        {
            var query = context.TicketPrivateFixedIncomes
                .Include(t => t.OperationType)
                .Include(t => t.Ticket);

            var result = await ticketQueryPaginatedService.By(query)
                                                          .Using(filter)
                                                          .ListPaginatedAsync(username);

            return result;
        }

        [NonAction]
        public async Task<TicketPrivateFixedIncomeModel> EditAsync(string username, int ticketId, TicketPrivateFixedIncomeModel ticketData)
        {
            var accountManager = accountManagerService.GetAccountManager(username);

            var ticket = await context.TicketPrivateFixedIncomes
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

            var result = mapper.Map<TicketPrivateFixedIncomeModel>(ticket);

            return await Task.FromResult(result);
        }

        [NonAction]
        public async Task<TicketPrivateFixedIncomeModel> RegisterNewAsync(string username, TicketPrivateFixedIncomeModel ticketData)
        {
            var accountManager = accountManagerService.GetAccountManager(username);

            var newTicket = mapper.Map<TicketPrivateFixedIncome>(ticketData);

            if (newTicket.Ticket.OperationDate == null || newTicket.Ticket.OperationDate == DateTimeOffset.MinValue)
            {
                newTicket.Ticket.OperationDate = DateTimeOffset.Now;
            }

            newTicket.Ticket.Type = new TicketType { Id = (short)TicketTypes.PrivateFixedIncome };
            newTicket.Ticket.AccountManager = accountManager;
            newTicket.Ticket.AccountManagerName = accountManager.Name;
            newTicket.Ticket.AccountManagerDocument = accountManager.Document;

            context.Attach(newTicket.OperationType);
            context.Attach(newTicket.Ticket.Type);
            context.Attach(newTicket.Ticket.AccountManager);

            context.TicketPrivateFixedIncomes.Add(newTicket);
            await context.SaveChangesAsync();

            var result = mapper.Map<TicketPrivateFixedIncomeModel>(newTicket);

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
            var current = await context.TicketPrivateFixedIncomes
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

            var workflowId = await wfMethod((int)WorkflowTypes.PrivateFixedIncome, username);

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
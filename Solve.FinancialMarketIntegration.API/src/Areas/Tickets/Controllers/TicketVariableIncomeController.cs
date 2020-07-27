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
using AutoMapper;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Filters;
using Solve.FinancialMarketIntegration.API.Areas.Security.Filters;
using Solve.FinancialMarketIntegration.API.Areas.Security.Services;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Events;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Events.DomainEvents;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Controllers
{
    [Authorize]
    [Route("/api/v1/tickets/variable-income")]
    [ApiController]
    [TicketActionExceptionFilter]
    public class TicketVariableIncomeController : ControllerBase, ITicketService<TicketVariableIncomeModel>, ITicketBulkService<TicketVariableIncomeModel>
    {
        private TicketDataContext context;
        private IWorkflowService workflowService;
        private IAccountManagerService accountManagerService;
        private IMapper mapper;

        private TicketListQueryService<TicketVariableIncome, TicketVariableIncomeModel> ticketQueryPaginatedService;

        public TicketVariableIncomeController(
            TicketDataContext context,
            IWorkflowService workflowService,
            IAccountManagerService accountManagerService,
            IMapper mapper,
            TicketListQueryService<TicketVariableIncome, TicketVariableIncomeModel> ticketQueryPaginatedService
        )
        {
            this.context = context;
            this.workflowService = workflowService;
            this.accountManagerService = accountManagerService;
            this.mapper = mapper;
            this.ticketQueryPaginatedService = ticketQueryPaginatedService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id, int? page, int? pageSize)
        {
            var result = await this.GetTicketAsync(id, User.Identity.Name, page, pageSize);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketVariableIncomeModel>>> Get([FromQuery]TicketFilter filter)
        {
            var result = await ListAsync(User.Identity.Name, filter);
            return Ok(result);
        }

        [HttpGet("list-paginated")]
        public async Task<ActionResult<PagedResult<TicketVariableIncomeModel>>> ListPaginated([FromQuery]TicketFilter filter)
        {
            var result = await ListPaginatedAsync(
                User.Identity.Name,
                filter
            );
            return Ok(result);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(RequiredAccountManagerFilter))]
        public async Task<ActionResult<TicketVariableIncomeModel>> Put([FromRoute]int id, [FromBody]TicketVariableIncomeModel body)
        {
            var result = await EditAsync(User.Identity.Name, id, body);
            return Ok(result);
        }

        [HttpPost]
        [TypeFilter(typeof(RequiredRoleFilter), Arguments = new object[] { new[] { Roles.CreateVariableIncomeTicket } })]
        [TypeFilter(typeof(RequiredAccountManagerFilter))]
        public async Task<ActionResult<TicketVariableIncomeModel>> Post([FromServices]EventBus eventBus, [FromBody]TicketVariableIncomeModel body)
        {
            var result = await RegisterNewAsync(User.Identity.Name, body);
            eventBus.Dispatch(new TicketRegisteredEvent(ticketId: result.Id.Value, user: User.Identity.Name));

            return Ok(result);
        }

        [HttpPost("{id}/duplicate")]
        [TypeFilter(typeof(RequiredRoleFilter), Arguments = new object[] { new[] { Roles.CreateVariableIncomeTicket } })]
        [TypeFilter(typeof(RequiredAccountManagerFilter))]
        public async Task<ActionResult<TicketVariableIncomeModel>> Duplicate([FromRoute]int id)
        {
            var result = await DuplicateAsync(User.Identity.Name, id);
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

        [NonAction]
        public Task<TicketVariableIncomeModel> GetByIdAsync(string username, int ticketId)
        {
            return GetTicketAsync(ticketId, username, null, null);
        }

        [HttpGet("{id}/items")]
        public async Task<ActionResult<PagedResult<TicketVariableIncomeItemModel>>> ListTicketVariableIncomeItemById(int id, int? page, int? pageSize)
        {
            return Ok(await context.TicketVariableIncomeItems
                             .Where(x => x.TicketId == id)
                             .Select(i => mapper.Map<TicketVariableIncomeItemModel>(i))
                             .IQueryableGetPaged(page, pageSize));
        }

        [NonAction]
        public async Task<IEnumerable<TicketVariableIncomeModel>> ListAsync(
           string username,
           TicketFilter filter
       )
        {
            var accountManager = accountManagerService.GetAccountManager(username);
            var query = context.TicketVariableIncome
                .AsNoTracking()
                .ApplyFilter(filter, accountManager)
                .Include(t => t.LineItems)
                .Include(t => t.Ticket);

            var data = await query.ToListAsync();

            if (data.Count == 0)
            {
                return await Task.FromResult(new TicketVariableIncomeModel[] { });
            }

            var ids = data
                .Where(d => d.Ticket.WorkflowId != null)
                .Select(d => (int)d.Ticket.WorkflowId);

            var workflowDetails = (await workflowService.GetWorkflowsDetailAsync(ids)).ToDictionary(kvp => kvp.WorkflowId, kvp => kvp);

            var result = data
                .Select(ticket =>
                {
                    var mapped = mapper.Map<TicketVariableIncomeModel>(ticket);

                    mapped.MapWorkflowData(workflowDetails, ticket.Ticket.WorkflowId);

                    return mapped;
                })
                .FilterWorkflowStatus(filter.StatusId);

            return result.ToList();
        }

        [NonAction]
        public async Task<PagedResult<TicketVariableIncomeModel>> ListPaginatedAsync(
            string username,
            TicketFilter filter
        )
        {
            var query = context.TicketVariableIncome
                .Include(t => t.Ticket)
                .Include(t => t.LineItems);

            var result = await ticketQueryPaginatedService.By(query)
                                                          .Using(filter)
                                                          .ListPaginatedAsync(username);
            return result;
        }

        [NonAction]
        public async Task<TicketVariableIncomeModel> RegisterNewAsync(string username, TicketVariableIncomeModel ticketData)
        {
            var accountManager = accountManagerService.GetAccountManager(username);

            var newTicket = mapper.Map<TicketVariableIncome>(ticketData);

            if (newTicket.Ticket.OperationDate == null || newTicket.Ticket.OperationDate == DateTimeOffset.MinValue)
            {
                newTicket.Ticket.OperationDate = DateTimeOffset.Now;
            }

            newTicket.Ticket.Type = new TicketType { Id = (short)TicketTypes.VariableIncome };
            newTicket.Ticket.AccountManager = accountManager;
            newTicket.Ticket.AccountManagerName = accountManager.Name;
            newTicket.Ticket.AccountManagerDocument = accountManager.Document;

            newTicket.SellTotal = ticketData.Items.Where(t => t.BuySell == "V").Sum(t => t.Amount.Value * t.Price);
            newTicket.BuyTotal = ticketData.Items.Where(t => t.BuySell == "C").Sum(t => t.Amount.Value * t.Price);

            context.Attach(newTicket.Ticket.Type);
            context.Attach(newTicket.Ticket.AccountManager);

            if (newTicket.Ticket.Portfolio == null) {
                newTicket.Ticket.Portfolio = new Portfolio() {
                    Account = "",
                    ClearingAccount = "",
                    Document = "",
                    Name = "",
                    Code = ""
                };
            }

            context.TicketVariableIncome.Add(newTicket);
            await context.SaveChangesAsync();

            var result = mapper.Map<TicketVariableIncomeModel>(newTicket);

            return result;
        }

        [NonAction]
        public async Task<TicketVariableIncomeModel> DuplicateAsync(string username, int id)
        {
            var ticket = await context.TicketVariableIncome
                .AsNoTracking()
                .Include(t => t.Ticket)
                .Include(t => t.LineItems)
                .FirstOrDefaultAsync(t => t.Ticket.Id == id);

            if (ticket == null) {
                throw new Exception("not-found");
            }

            ticket.Ticket.Id = 0;
            ticket.Ticket.WorkflowId = null;

            foreach (var item in ticket.LineItems)
            {
                item.Id = 0;
                item.TicketId = 0;
            }

            var accountManager = accountManagerService.GetAccountManager(username);

            ticket.Ticket.Type = new TicketType { Id = (short)TicketTypes.VariableIncome };
            ticket.Ticket.AccountManager = accountManager;
            ticket.Ticket.AccountManagerName = accountManager.Name;
            ticket.Ticket.AccountManagerDocument = accountManager.Document;

            context.Attach(ticket.Ticket.Type);
            context.Attach(ticket.Ticket.AccountManager);

            context.TicketVariableIncome.Add(ticket);

            await context.SaveChangesAsync();

            var result = mapper.Map<TicketVariableIncomeModel>(ticket);

            return result;
        }

        [NonAction]
        public async Task<TicketVariableIncomeModel> EditAsync(string username, int ticketId, TicketVariableIncomeModel ticketData)
        {
            var accountManager = accountManagerService.GetAccountManager(username);

            var ticket = await context.TicketVariableIncome
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

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var actualData = ticketData;
                    actualData.Items = ticketData.GetPendingInsertion().ToList();

                    mapper.Map(actualData, ticket);

                    if (ticketData?.Deleted.Count() > 0) {
                        var toRemove = context.TicketVariableIncomeItems
                            .Where(t => ticketData.Deleted.Contains(t.Id) && t.TicketId == ticket.Ticket.Id);

                        context.TicketVariableIncomeItems.RemoveRange(toRemove);
                    }

                    await context.SaveChangesAsync();

                    ticket.BuyTotal = await context.TicketVariableIncomeItems
                        .Where(i => i.TicketId == ticket.Ticket.Id && i.BuyOrSell == "C")
                        .SumAsync(i => i.Amount * i.Price);

                    ticket.SellTotal = await context.TicketVariableIncomeItems
                        .Where(i => i.TicketId == ticket.Ticket.Id && i.BuyOrSell == "V")
                        .SumAsync(i => i.Amount * i.Price);

                    await context.SaveChangesAsync();

                    transaction.Commit();

                    var result = mapper.Map<TicketVariableIncomeModel>(ticket);

                    result.Items.AddRange(ticketData.GetExisting());                    

                    return result;
                }
                catch (System.Exception)
                {
                    transaction.Rollback();                    
                    throw;
                }                
            }
        }

        [NonAction]
        public async Task<string> ForwardAsync(string username, int ticketId)
        {
            var current = await context.TicketVariableIncome
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

            var workflowId = await workflowService.OpenAsync((int)WorkflowTypes.VariableIncomeByManager, username);

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

            var workflow = await workflowService.GetWorkflowDetailsAsync(workflowId);
            return workflow.StatusDescription;
        }                

        [NonAction]
        public async Task<IList<TicketVariableIncomeModel>> RegisterNewBulkAsync(string username, IList<TicketVariableIncomeModel> ticketData)
        {
            IList<TicketVariableIncomeModel> result = new List<TicketVariableIncomeModel>();
            var ticketType = new TicketType { Id = (short)TicketTypes.VariableIncome };
            context.Attach(ticketType);

            foreach (TicketVariableIncomeModel ticket in ticketData)
            {
                result.Add(await this.RegisterFromData(username, ticket, false, ticketType));
            }

            await this.context.SaveChangesAsync();
            return result;
        }

        private async Task<TicketVariableIncomeModel> GetTicketAsync(int id, string username, int? page, int? pageSize)
        {
            var accountManager = accountManagerService.GetAccountManager(username);
            var queryTicket = this.context.TicketVariableIncome
                                        .Include(t => t.Ticket)
                                        .Include(t => t.LineItems)
                                        .Where(t => t.Ticket.Id == id);
            if (accountManager != null)
            {
                queryTicket = queryTicket.Where(t => t.Ticket.AccountManager.Id == accountManager.Id);
            }
            var ticket = await queryTicket.SingleOrDefaultAsync();

            if (ticket == null)
            {
                throw new Exception("not-found");
            }

            var ticketModel = mapper.Map<TicketVariableIncomeModel>(ticket);

            if (ticket.Ticket.WorkflowId != null)
            {
                ticketModel.MapWorkflowData(await workflowService.GetWorkflowDetailsAsync(ticket.Ticket.WorkflowId.Value));
            }


            var pagedResult = await context.TicketVariableIncomeItems
                                     .Where(x => x.TicketId == ticket.Ticket.Id)
                                     .Select(i => mapper.Map<TicketVariableIncomeItemModel>(i))
                                     .IQueryableGetPaged(page, pageSize);

            ticketModel.PagedResultItems = pagedResult;

            return ticketModel;
        }

        private async Task<TicketVariableIncomeModel> RegisterFromData(string username, TicketVariableIncomeModel ticketData, bool persistData = true, TicketType ticketType = null)
        {
            var accountManager = accountManagerService.GetAccountManager(username);

            if (ticketType == null)
            {
                ticketType = new TicketType { Id = (short)TicketTypes.VariableIncome };
                context.Attach(ticketType);
            }

            var ticket = new Ticket
            {
                Type = ticketType,
                Portfolio = new Portfolio("", ticketData.ClientCode, "", "", "", "", ""),
                OperationDate = DateTimeOffset.Now,
                Annotations = "",
                AccountManager = accountManager,
                AccountManagerName = accountManager.Name,
                AccountManagerDocument = accountManager.Document
            };

            var ticketVariableIncome = new TicketVariableIncome
            {
                Ticket = ticket,
                BrokerCode = ticketData.BrokerCode,
                StockExchangeDate = ticketData.StockExchangeDate,
                ClientCode = ticketData.ClientCode,
                ClientCodeDigit = ticketData.ClientCodeDigit,
                SellTotal = ticketData.Items.Where(t => t.BuySell == "V").Sum(t => t.Amount.Value * t.Price),
                BuyTotal = ticketData.Items.Where(t => t.BuySell == "C").Sum(t => t.Amount.Value * t.Price),
                LineItems = new List<TicketVariableIncomeItem>()
            };
            await context.TicketVariableIncome.AddAsync(ticketVariableIncome);

            ticketData.Items.ForEach(item =>
            {
                var ticketItem = new TicketVariableIncomeItem()
                {
                    TradingCode = item.TradingCode,
                    TradingCodeBusinessCode = item.TradingCodeBusinessCode,
                    BuyOrSell = item.BuySell,
                    Amount = item.Amount.Value,
                    SettlementTypeOfSecondaryTerm = item.SettlementTypeOfSecondaryTerm,
                    Price = item.Price,
                    Factor = item.Factor.Value,
                    SettlementType = item.SettlementType,
                    AssetCode = item.AssetCode,
                    ISINCode = item.ISINCode,
                    ISINCodeDistribution = item.ISINCodeDistribution,
                    CompanyName = item.CompanyName,
                    Specification = item.Specification,
                    SpecificationIndicator = item.SpecificationIndicator,
                    IsAfterMarket = item.IsAfterMarket,
                    MarketType = item.MarketType
                };
                
                ticketVariableIncome.LineItems.Add(ticketItem);
            });

            if (persistData)
            {
                await context.SaveChangesAsync();
            }

            var result = new TicketVariableIncomeModel
            {
                Id = ticket.Id,
                StatusId = null,
                StatusRequiredRoles = null
            };

            if (ticket.WorkflowId.HasValue)
            {
                var workflowDetails = await workflowService.GetWorkflowDetailsAsync(ticket.WorkflowId.Value);
                result.StatusId = workflowDetails.StatusId;
                result.StatusRequiredRoles = workflowDetails.StatusRequiredRoles;
            }

            return result;
        }
    }
}

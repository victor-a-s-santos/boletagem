using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Services
{
    public class TicketListQueryService<E, M>
        where E : class, ITicketable
        where M : class, ITicketModel
    {
        private IAccountManagerService accountManagerService;
        private IWorkflowService workflowService;
        private IMapper mapper;
        private IQueryable<E> query;
        private List<WorkflowDetails> workflowStatusServiceResult;
        private TicketFilter filter;

        private IEnumerable<int> workflowIds;

        public TicketListQueryService(IAccountManagerService accountManagerService, IWorkflowService workflowService, IMapper mapper)
        {
            this.accountManagerService = accountManagerService;
            this.workflowService = workflowService;
            this.mapper = mapper;

            workflowStatusServiceResult = new List<WorkflowDetails>();
        }

        public TicketListQueryService<E, M> By(IQueryable<E> query)
        {
            this.query = query;
            return this;
        }

        public TicketListQueryService<E, M> Using(TicketFilter filter)
        {
            this.filter = filter;
            return this;
        }

        public async Task<IEnumerable<M>> ListAsync(string username)
        {
            var data = GetData(username, filter);

            if (data.Count() == 0)
                return new M[] { };

            var workflowStatus = await CheckPreloadWorkflowStatus(username, data);

            var result = data
                .Select(MapProjection(workflowStatus));

            return result;
        }

        public async Task<PagedResult<M>> ListPaginatedAsync(string username)
        {
            var data = GetData(username, filter);

            if (data.Count() == 0)
                return new PagedResult<M>();

            var workflowStatus = await CheckPreloadWorkflowStatus(username, data);

            var result = data.Select(MapProjection(workflowStatus))
                              .GetPaged(filter.Page, filter.PageSize);

            return result;
        }

        private System.Func<E, M> MapProjection(Dictionary<int, WorkflowDetails> workflowDetails)
        {
            return ticket =>
            {
                var mapped = mapper.Map<M>(ticket);
                mapped.MapWorkflowData(workflowDetails, ticket.Ticket.WorkflowId);
                return mapped;
            };
        }

        private async Task<Dictionary<int, WorkflowDetails>> CheckPreloadWorkflowStatus(string username, IQueryable<E> data)
        {
            if (workflowStatusServiceResult.Any() == false)
            {
                var ids = await data
                    .Where(d => d.Ticket.WorkflowId != null)
                    .Select(d => (int)d.Ticket.WorkflowId)
                    .ToListAsync();

                workflowStatusServiceResult.AddRange(await workflowService.GetWorkflowsDetailAsync(ids));
            }

            return workflowStatusServiceResult.ToDictionary(kvp => kvp.WorkflowId, kvp => kvp);
        }

        private IQueryable<E> GetData(string username, TicketFilter filter)
        {
            var accountManager = accountManagerService.GetAccountManager(username);
            if (filter.StatusId == -1)
            {
                query = query.Where(t => t.Ticket.WorkflowId == null);
            }
            else if (filter.StatusId > 0)
            {
                var status = GetWorkflowStatus();
                query = query.ByWorkflows(status.Select(s => s.WorkflowId));
            }

            return query.ApplyFilter(filter, accountManager);
        }

        private IEnumerable<WorkflowDetails> GetWorkflowStatus()
        {
            if (workflowStatusServiceResult.Any())
                return workflowStatusServiceResult;


            if (filter.StatusId > 0)
            {
                workflowStatusServiceResult.AddRange(workflowService.GetWorkflowsBy(filter.StatusId.Value, filter.StartDate.GetValueOrDefault(), filter.EndDate.GetValueOrDefault()));
            }
            else if (workflowIds.Any())
            {
                workflowStatusServiceResult.AddRange(workflowService.GetWorkflowsDetail(workflowIds));
            }

            return workflowStatusServiceResult;
        }
    }
}

using Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Services
{

    public interface ITicketService<T> where T: class, ITicketModel {

        Task<T> GetByIdAsync(string username, int ticketId);

        Task<PagedResult<T>> ListPaginatedAsync(string username, TicketFilter filter);

        Task<IEnumerable<T>> ListAsync(string username, TicketFilter filter);

        Task<T> EditAsync(string username, int ticketId, T ticketData);

        Task<T> RegisterNewAsync(string username, T ticketData);

        Task<string> ForwardAsync(string username, int ticketId);
    }

    public interface ITicketModel {
        int? Id { get; set; }
        DateTimeOffset? OperationDate { get; set; }
        DateTimeOffset? WorkflowStartDate { get; set; }
        DateTimeOffset? WorkflowEndDate { get; set; }
        int? StatusId { get; set; }
        string StatusDescription { get; set; }
        IEnumerable<int> StatusRequiredRoles { get; }

        void MapWorkflowData(IDictionary<int, WorkflowDetails> source, int? workflowId);
        void MapWorkflowData(WorkflowDetails source);
    }
}
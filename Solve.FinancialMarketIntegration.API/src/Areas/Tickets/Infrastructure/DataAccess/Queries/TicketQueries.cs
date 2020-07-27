using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Controllers;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Models;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;

public static class TicketQueries
{
    public static DateTimeOffset EndOfDay(this DateTimeOffset date)
    {
        return date.AddDays(1).AddTicks(-1);
    }

    public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> source, TicketFilter filter, AccountManager accountManager)
        where T : ITicketable
    {
        if (filter.StartDate != null)
        {
            source = source.Where(t => t.Ticket.OperationDate >= filter.StartDate.Value);
        }

        if (filter.EndDate != null)
        {
            source = source.Where(t => t.Ticket.OperationDate <= filter.EndDate.Value.EndOfDay());
        }

        if (filter.PortfolioCode != null)
        {
            source = source.Where(t => t.Ticket.Portfolio.Code.Contains(filter.PortfolioCode));
        }

        if (filter.PortfolioName != null)
        {
            source = source.Where(t => t.Ticket.Portfolio.Name.Contains(filter.PortfolioName));
        }

        if (filter.PortfolioDocument != null)
        {
            source = source.Where(t => t.Ticket.Portfolio.Document.Contains(filter.PortfolioDocument));
        }

        if (filter.TicketId != null)
        {
            source = source.Where(t => t.Ticket.Id == filter.TicketId.Value);
        }

        if (accountManager != null)
        {
            source = source.Where(t => t.Ticket.AccountManager.Id == accountManager.Id);
        }
        else if(filter.AccountManagerId != null) {
            source = source.Where(t => t.Ticket.AccountManager.Id == filter.AccountManagerId.Value);
        }

        return source;
    }

    public static IQueryable<T> ByWorkflows<T>(this IQueryable<T> source, IEnumerable<int> workflows)
        where T : ITicketable
    {
        source = source.Where(t => workflows.Any(w => w == t.Ticket.WorkflowId));
        return source;
    }

    public static IEnumerable<T> FilterWorkflowStatus<T>(this IEnumerable<T> source, int? statusId)
        where T: ITicketModel
    {
        if (statusId != null) {
            if (statusId > 0) {
                source = source.Where(s => s.StatusId == statusId);
            }
            if (statusId == -1) {
                source = source.Where(s => s.StatusId == null);
            }
        }

        return source;
    }
}

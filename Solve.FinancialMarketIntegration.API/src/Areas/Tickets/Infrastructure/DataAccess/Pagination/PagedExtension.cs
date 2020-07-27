using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess
{
    public static class PagedExtension
    {
        public static PagedResult<T> GetPaged<T>(this IEnumerable<T> list,
                                        int? page, int? pageSize) where T : class
        {
            var pagedResult = new PagedResult<T>();

            if (page.HasValue && pageSize.HasValue)
            {
                pagedResult.CurrentPage = page.Value;
                pagedResult.PageSize = pageSize.Value;
                pagedResult.RowCount = list.Count();

                var pageCount = (double)pagedResult.RowCount / pageSize.Value;
                pagedResult.PageCount = (int)Math.Ceiling(pageCount);

                var skip = (page.Value - 1) * pageSize.Value;
                pagedResult.Results = list.Skip(skip).Take(pageSize.Value).ToList();
            }
            else
                pagedResult.Results = list.ToList();

            return pagedResult;
        }

        public static async Task<PagedResult<T>> IQueryableGetPaged<T>(this IQueryable<T> query,
                                        int? page, int? pageSize) where T : class
        {
            var pagedResult = new PagedResult<T>();

            if (page.HasValue && pageSize.HasValue)
            {
                pagedResult.CurrentPage = page.Value;
                pagedResult.PageSize = pageSize.Value;
                pagedResult.RowCount = query.Count();

                var pageCount = (double)pagedResult.RowCount / pageSize.Value;
                pagedResult.PageCount = (int)Math.Ceiling(pageCount);

                var skip = (page.Value - 1) * pageSize.Value;
                pagedResult.Results = await query.Skip(skip).Take(pageSize.Value).ToListAsync();
            }
            else
                pagedResult.Results = await query.ToListAsync();

            return pagedResult;
        }
    }
}

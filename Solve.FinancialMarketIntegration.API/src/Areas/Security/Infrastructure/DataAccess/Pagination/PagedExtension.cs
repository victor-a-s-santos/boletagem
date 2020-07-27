using System;
using System.Linq;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess
{
    public static class PagedExtension
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query,
                                        int page, int pageSize) where T : class
        {
            var pagedResult = new PagedResult<T>();

            if (page != 0 && pageSize != 0)
            {
                pagedResult.CurrentPage = page;
                pagedResult.PageSize = pageSize;
                pagedResult.RowCount = query.Count();

                var pageCount = (double)pagedResult.RowCount / pageSize;
                pagedResult.PageCount = (int)Math.Ceiling(pageCount);

                var skip = (page - 1) * pageSize;
                pagedResult.Results = query.Skip(skip).Take(pageSize).ToList();
            }
            else
                pagedResult.Results = query.ToList();

            return pagedResult;
        }
    }
}

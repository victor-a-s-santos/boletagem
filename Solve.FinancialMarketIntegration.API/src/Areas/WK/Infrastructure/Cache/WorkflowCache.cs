using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.Cache
{
    public class WorkflowCache
    {
        private readonly IMemoryCache cache;
        private readonly WorkflowDataContext context;
        public static CacheKey<WorkflowDetails, int> Workflow;

        public WorkflowCache(IMemoryCache cache, WorkflowDataContext context)
        {
            this.cache = cache;
            this.context = context;
        }

        public async Task<WorkflowDetails> GetAsync(int workflowId)
        {
            var key = new CacheKey<WorkflowDetails, int>(workflowId);
            if (!cache.TryGetValue(key, out WorkflowDetails result))
                result = await LoadAsync(workflowId);

            return result;
        }

        public async Task<WorkflowDetails> RefreshAsync(int workflowId)
        {
            return await LoadAsync(workflowId);
        }

        private async Task<WorkflowDetails> LoadAsync(int workflowId)
        {
            var result = await context.Workflows.Where(w => w.Id == workflowId)
                                      .Select(WorkflowQueries.WorkflowToStepProjection)
                                      .FirstOrDefaultAsync();

            var options = new MemoryCacheEntryOptions();
            options.SetSlidingExpiration(TimeSpan.FromMinutes(15));

            var key = new CacheKey<WorkflowDetails, int>(workflowId);
            cache.Set(key, result, options);

            return result;
        }

        public void Remove(int workflowId)
        {
            var key = new CacheKey<WorkflowDetails, int>(workflowId);
            cache.Remove(key);
        }
    }
}

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        [DataMember(Name = "results", IsRequired = false)]
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}

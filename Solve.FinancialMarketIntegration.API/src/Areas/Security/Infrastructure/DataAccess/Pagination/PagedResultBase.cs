using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess
{
    [DataContract]
    public abstract class PagedResultBase
    {
        [DataMember(Name = "currentPage", IsRequired = false)]
        public int CurrentPage { get; set; }

        [DataMember(Name = "pageCount", IsRequired = false)]
        public int PageCount { get; set; }

        [DataMember(Name = "pageSize", IsRequired = false)]
        public int PageSize { get; set; }

        [DataMember(Name = "rowCount", IsRequired = false)]
        public int RowCount { get; set; }

        [DataMember(Name = "firstRowOnPage", IsRequired = false)]
        public int FirstRowOnPage
        {
            get { return (CurrentPage - 1) * PageSize + 1; }
            set { }
        }

        [DataMember(Name = "lastRowOnPage", IsRequired = false)]
        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
            set { }
        }
    }
}
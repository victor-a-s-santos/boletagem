using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
    public class TicketFilter
    { 
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public int? StatusId { get; set; }
        public string PortfolioCode { get; set; }
        public string PortfolioName { get; set; }
        public string PortfolioDocument { get; set; }
        public int? TicketId { get; set; }
        public int? AccountManagerId { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}

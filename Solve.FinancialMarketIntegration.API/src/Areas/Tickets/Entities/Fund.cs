using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class Fund : Entity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Code { get; set; }
        public string CetipAccount { get; set; } // Conta Clearing
        public bool IsFIDC { get; set; }
        public string Class { get; set; }
        public string CodeIssuer { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public bool IsNewIssue { get; set; }
        public string Type { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class TicketFundQuotas : Ticketable
    {
        public OperationType OperationType { get; set; }
        public SettlementType SettlementType { get; set; }
        public FundInfo Fund { get; set; }
        public bool? FullRedeem { get; set; }
        public bool IsSecondaryMarket { get; set; }
        public bool IsIssueUnitPrice { get; set; }
        public decimal OperationValue { get; set; }
        public decimal? Amount { get; set; }
        public DateTimeOffset? SettlementDate { get; set; }
        public DateTimeOffset? QuotationDate { get; set; }
        public decimal? QuotaValue { get; set; }
        public bool? HasSameOwnership { get; set; }
        public bool? IsCetipVoice { get; set; }
        public string CetipVoiceId { get; set; }

        public Counterpart Counterpart { get; set; }

        public class FundInfo
        {
            public string Name { get; set; }
            public string Document { get; set; }
            public bool IsFIDC { get; set; }
            public string ClassSeries { get; set; }
            public string IssuerName { get; set; }
            public bool IsNewFund { get; set; }
            public string Type { get; set; }
            public string Branch { get; set; }
            public string Bank { get; set; }
            public string Account { get; set; }
            public string MnemonicCode { get; set; }

        }

    }
}

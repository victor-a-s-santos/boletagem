using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class TicketPrivateFixedIncome : Ticketable
    {
        public OperationType OperationType { get; set; }

        public decimal Amount { get; set; }

        public decimal UnitPrice { get; set; }

        public bool IsSecondaryMarket { get; set; }

        public bool IsTerm { get; set; }

        public DateTimeOffset? AcquisitionDate { get; set; }

        public decimal OperationValue { get; set; }

        public string AssetType { get; set; }

        public string AssetCode { get; set; }

        public DateTimeOffset ExpirationDate { get; set; }

        public decimal IssueFee { get; set; }

        public DateTimeOffset IssueDate { get; set; }

        public Counterpart Counterpart { get; set; }

        public string ObjectAction { get; set; }
        public string Index { get; set; }
        public decimal? IndexPercent { get; set; }
        public string Issuer { get; set; }
        public int? IndexBase { get; set; }
        public int? InterestType { get; set; }
    }
}
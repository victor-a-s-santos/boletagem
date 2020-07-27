using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities.Enums;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class TicketCurrencyTerm : Ticketable
    {        
        public decimal OperationValue { get; set; }

        public OperationType OperationType { get; set; }

        public DateTimeOffset ExpirationDate { get; set; }

        public bool CetipSettlement { get; set; }

        public string ContractNumber { get; set; }

        public decimal FutureFee { get; set; }

        public int QuoteExpirationTypeId { get; set; }

        public Currency CurrencyId { get; set; }

        public bool CrossRate { get; set; }

        public Counterpart Counterpart { get; set; }
    }
}
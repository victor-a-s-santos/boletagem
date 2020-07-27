using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities
{
    public class TicketVariableIncomeItem
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string TradingCode { get; set; }
        public string TradingCodeBusinessCode { get; set; }
        public string BuyOrSell { get; set; }
        public int Amount { get; set; }
        public string SettlementTypeOfSecondaryTerm { get; set; }
        public decimal Price { get; set; }
        public int Factor { get; set; }
        public string SettlementType { get; set; }
        public string AssetCode { get; set; }
        public string ISINCode { get; set; }
        public string ISINCodeDistribution { get; set; }
        public string CompanyName { get; set; }
        public string Specification { get; set; }
        public string SpecificationIndicator { get; set; }
        public string IsAfterMarket { get; set; }
        public string MarketType { get; set; }
        public TicketVariableIncome TicketVariableIncome { get; set; }
    }
}

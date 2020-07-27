using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.Files.Entities
{

    public class FilePESC : IDisposable
    {
        public int Id { get; set; }
        public int IdFile { get; set; }
        public string BrokerCode { get; set; }
        public DateTimeOffset DateFile { get; set; }
        public DateTimeOffset DateMarket { get; set; }
        public string TradingCode { get; set; }
        public string TradingCodeBusinessCode { get; set; }
        public string BuyOrSell { get; set; }
        public string ClientCode { get; set; }
        public string ClientCodeDigit { get; set; }
        public int Amount { get; set; }
        public string PortfolioCode { get; set; }
        public string PortfolioCodeDigit { get; set; }
        public string CustodianUserCode { get; set; }
        public string CustodianClientCode { get; set; }
        public string CustodianClientCodeDigit { get; set; }
        public string SettlementTypeOfSecondaryTerm { get; set; }
        public string MarketType { get; set; }
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
        public string StockExchangeCode { get; set; }

        public void Dispose()
        {
            
        }
    }
   
}

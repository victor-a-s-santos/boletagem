using Newtonsoft.Json;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
    [DataContract]
    public class TicketFuturesModel : TicketModel
    {
        [DataMember(Name = "operationTypeId")]
        public short OperationTypeId { get; set; }

        [DataMember(Name = "amount")]
        public int? Amount { get; set; }

        [DataMember(Name = "unitPrice")]
        public decimal? UnitPrice { get; set; }

        [DataMember(Name = "tradingDate")]
        public DateTimeOffset? TradingDate { get; set; }

        [DataMember(Name = "percentageDiscount")]
        public decimal? PercentageDiscount { get; set; }

        [DataMember(Name = "paperCode")]
        public string PaperCode { get; set; }

        [DataMember(Name = "paperSerie")]
        public string PaperSerie { get; set; }

        [DataMember(Name = "annotations")]
        public string Annotations { get; set; }

        [DataMember(Name = "broker")]
        public string Broker { get; set; }

        [DataMember(Name = "brokerCode")]
        public string BrokerCode { get; set; }

        [DataMember(Name = "brokerAccount")]
        public string BrokerAccount { get; set; }

        [DataMember(Name = "brokerDocument")]
        public string BrokerDocument { get; set; }

        [DataMember(Name = "portfolioCode")]
        public string PortfolioCode { get; set; }

        [DataMember(Name = "portfolioName")]
        public string PortfolioName { get; set; }

        [DataMember(Name = "portfolioDocument")]
        public string PortfolioDocument { get; set; }

        [DataMember(Name = "portfolioBank")]
        public string PortfolioBank { get; set; }

        [DataMember(Name = "portfolioBranch")]
        public string PortfolioBranch { get; set; }

        [DataMember(Name = "portfolioAccount")]
        public string PortfolioAccount { get; set; }

        [DataMember(Name = "portfolioB3Account")]
        public string PortfolioB3Account { get; set; }

        [DataMember(Name = "accountManagerName")]
        public string AccountManagerName { get; set; }

        [DataMember(Name = "accountManagerDocument")]
        public string AccountManagerDocument { get; set; }
    }
}

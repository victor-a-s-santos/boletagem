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
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
    [DataContract]
    public class TicketMarginModel : TicketModel
    {
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

        [DataMember(Name = "portfolioClearingAccount")]
        public string PortfolioClearingAccount { get; set; }

        [DataMember(Name = "operationTypeId")]
        public short OperationTypeId { get; set; }

        [DataMember(Name = "marketTypeId")]
        public short MarketTypeId { get; set; }

        [DataMember(Name = "amount")]
        public decimal? Amount { get; set; }
        
        [DataMember(Name = "unitPrice")]
        public decimal? UnitPrice { get; set; }
        
        [DataMember(Name = "operationValue")]
        public decimal? OperationValue { get; set; }
        
        [DataMember(Name = "securityType")]
        public string SecurityType { get; set; }

        [DataMember(Name = "securityName")]
        public string SecurityName { get; set; }

        [DataMember(Name = "securityCode")]
        public string SecurityCode { get; set; }

        [DataMember(Name = "dueDate")]
        public DateTimeOffset? DueDate { get; set; }

        [DataMember(Name = "purchaseDate")]
        public DateTimeOffset? PurchaseDate { get; set; }

        [DataMember(Name = "command")]
        public string Command { get; set; }

        [DataMember(Name = "quotation")]
        public decimal? Quotation { get; set; }
        
        [DataMember(Name = "asset")]
        public string Asset { get; set; }

        [DataMember(Name = "counterpartName")]
        public string CounterpartName { get; set; }

        [DataMember(Name = "counterpartDocument")]
        public string CounterpartDocument { get; set; }

        [DataMember(Name = "counterpartClearingAccount")]
        public string CounterpartClearingAccount { get; set; }

        [DataMember(Name = "counterpartBrokerAccount")]
        public string CounterpartBrokerAccount { get; set; }

        [DataMember(Name = "annotations")]
        public string Annotations { get; set; }

        [DataMember(Name = "accountManagerName")]
        public string AccountManagerName { get; set; }

        [DataMember(Name = "accountManagerDocument")]
        public string AccountManagerDocument { get; set; }

        [DataMember(Name = "bank")]
        public string Bank { get; set; }

        [DataMember(Name = "branch")]
        public string Branch { get; set; }

        [DataMember(Name = "account")]
        public string Account { get; set; }

        [DataMember(Name = "index")]
        public string Index { get; set; }

        [DataMember(Name = "indexPercent")]
        public decimal? IndexPercent { get; set; }

        [DataMember(Name = "issuer")]
        public string Issuer { get; set; }

        [DataMember(Name = "indexBase")]
        public decimal? IndexBase { get; set; }

        [DataMember(Name = "interestType")]
        public decimal? InterestType { get; set; }
    }
}
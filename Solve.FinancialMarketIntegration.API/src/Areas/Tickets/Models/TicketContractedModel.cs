using Newtonsoft.Json;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
    [DataContract]
    public class TicketContractedModel : TicketModel
    {
        [DataMember(Name = "operationTypeId")]
        public short OperationTypeId { get; set; }

        [DataMember(Name = "amount")]
        public long? Amount { get; set; }

        [DataMember(Name = "unitPriceOutward")]
        public decimal? UnitPriceOutward { get; set; }

        [DataMember(Name = "unitPriceReturn")]
        public decimal? UnitPriceReturn { get; set; }

        [DataMember(Name = "returnDate")]
        public DateTimeOffset? ReturnDate { get; set; }

        [DataMember(Name = "valueOutward")]
        public decimal? ValueOutward { get; set; }

        [DataMember(Name = "valueReturn")]
        public decimal? ValueReturn { get; set; }

        [DataMember(Name = "operationValue")]
        public decimal? OperationValue { get; set; }

        [DataMember(Name = "security")]
        public string Security { get; set; }

        [DataMember(Name = "securityId")]
        public string SecurityId { get; set; }

        [DataMember(Name = "expirationDate")]
        public DateTimeOffset? ExpirationDate { get; set; }

        [DataMember(Name = "issueTax")]
        public decimal? IssueTax { get; set; }

        [DataMember(Name = "counterpartName")]
        public string CounterpartName { get; set; }

        [DataMember(Name = "counterpartDocument")]
        public string CounterpartDocument { get; set; }

        [DataMember(Name = "counterpartSelicAccount")]
        public string CounterpartSelicAccount { get; set; }

        [DataMember(Name = "command")]
        public string Command { get; set; }

        [DataMember(Name = "annotations")]
        public string Annotations { get; set; }

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

        [DataMember(Name = "portfolioSelicAccount")]
        public string PortfolioSelicAccount { get; set; }

        [DataMember(Name = "accountManagerName")]
        public string AccountManagerName { get; set; }

        [DataMember(Name = "accountManagerDocument")]
        public string AccountManagerDocument { get; set; }
    }
}

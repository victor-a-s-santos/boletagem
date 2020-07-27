using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using System.Collections.Generic;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
    [DataContract]
    public class TicketSwapCetipModel : TicketModel
    {
        [DataMember(Name = "expirationDate")]
        public DateTimeOffset ExpirationDate { get; set; }

        [DataMember(Name = "operationValue")]
        public decimal OperationValue { get; set; }

        [DataMember(Name = "command")]
        public string Command { get; set; }

        [DataMember(Name = "activeIndex")]
        public string ActiveIndex { get; set; }

        [DataMember(Name = "activeIndexPercent")]
        public decimal ActiveIndexPercent { get; set; }

        [DataMember(Name = "activeIndexTax")]
        public decimal ActiveIndexTax { get; set; }

        [DataMember(Name = "activeIndexBase")]
        public decimal ActiveIndexBase { get; set; }

        [DataMember(Name = "activeInterestType")]
        public decimal ActiveInterestType { get; set; }

        [DataMember(Name = "passiveIndex")]
        public string PassiveIndex { get; set; }

        [DataMember(Name = "passiveIndexPercent")]
        public decimal PassiveIndexPercent { get; set; }

        [DataMember(Name = "passiveIndexTax")]
        public decimal PassiveIndexTax { get; set; }

        [DataMember(Name = "passiveIndexBase")]
        public decimal PassiveIndexBase { get; set; }

        [DataMember(Name = "passiveInterestType")]
        public decimal PassiveInterestType { get; set; }

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

        [DataMember(Name = "portfolioCetipAccount")]
        public string PortfolioCetipAccount { get; set; }

        [DataMember(Name = "counterpartName")]
        public string CounterpartName { get; set; }

        [DataMember(Name = "counterpartDocument")]
        public string CounterpartDocument { get; set; }

        [DataMember(Name = "counterpartCetipAccount")]
        public string CounterpartCetipAccount { get; set; }

        [DataMember(Name = "accountManagerName")]
        public string AccountManagerName { get; set; }

        [DataMember(Name = "accountManagerDocument")]
        public string AccountManagerDocument { get; set; }
    }
}

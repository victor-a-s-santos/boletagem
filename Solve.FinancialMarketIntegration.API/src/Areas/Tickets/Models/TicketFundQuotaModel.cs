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
    public class TicketFundQuotaModel : TicketModel
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

        [DataMember(Name = "portfolioCetipAccount")]
        public string PortfolioCetipAccount { get; set; }

        [DataMember(Name = "operationTypeId")]
        public short OperationTypeId { get; set; }

        [DataMember(Name = "fullRedeem")]
        public bool? FullRedeem { get; set; }

        [DataMember(Name = "isSecondaryMarket")]
        public bool IsSecondaryMarket { get; set; }

        [DataMember(Name = "isIssueUnitPrice")]
        public bool IsIssueUnitPrice { get; set; }

        [DataMember(Name = "settlementTypeId")]
        public short SettlementTypeId { get; set; }

        [DataMember(Name = "hasSameOwnership")]
        public bool? HasSameOwnership { get; set; }

        [DataMember(Name = "amount")]
        public decimal? Amount { get; set; }

        [DataMember(Name = "quotaValue")]
        public decimal? QuotaValue { get; set; }

        [DataMember(Name="settlementDate")]
        public DateTimeOffset? SettlementDate {get; set;}

        [DataMember(Name="quotationDate")]
        public DateTimeOffset? QuotationDate {get; set;}

        [DataMember(Name = "operationValue")]
        public decimal OperationValue { get; set; }

        [DataMember(Name = "fundName")]
        public string FundName { get; set; }

        [DataMember(Name = "fundDocument")]
        public string FundDocument { get; set; }

        [DataMember(Name = "isFIDC")]
        public bool IsFIDC { get; set; }

        [DataMember(Name = "isCetipVoice")]
        public bool? IsCetipVoice { get; set; }

        [DataMember(Name = "fundClassSeries")]
        public string FundClassSeries { get; set; }

        [DataMember(Name = "issuerName")]
        public string IssuerName { get; set; }

        [DataMember(Name = "isNewFund")]
        public bool IsNewFund { get; set; }

        [DataMember(Name = "fundType")]
        public string FundType { get; set; }

        [DataMember(Name = "fundBank")]
        public string FundBank { get; set; }

        [DataMember(Name = "fundBranch")]
        public string FundBranch { get; set; }

        [DataMember(Name = "fundAccount")]
        public string FundAccount { get; set; }

        [DataMember(Name = "counterpartName")]
        public string CounterpartName { get; set; }

        [DataMember(Name = "counterpartDocument")]
        public string CounterpartDocument { get; set; }

        [DataMember(Name = "counterpartCetipAccount")]
        public string CounterpartCetipAccount { get; set; }

        [DataMember(Name = "mnemonicCode")]
        public string MnemonicCode { get; set; }

        [DataMember(Name = "command")]
        public string Command { get; set; }

        [DataMember(Name = "annotations")]
        public string Annotations { get; set; }

        [DataMember(Name = "accountManagerName")]
        public string AccountManagerName { get; set; }

        [DataMember(Name = "accountManagerDocument")]
        public string AccountManagerDocument { get; set; }

        [DataMember(Name = "cetipVoiceId")]
        public string CetipVoiceId { get; set; }
    }
}
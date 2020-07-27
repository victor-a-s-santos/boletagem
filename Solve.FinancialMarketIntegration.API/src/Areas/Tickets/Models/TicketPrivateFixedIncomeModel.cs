using Newtonsoft.Json;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
    public class TicketPrivateFixedIncomeModel : TicketModel
    {
        [DataMember(Name = "portfolioName")]
        public string PortfolioName { get; set; }

        [DataMember(Name = "portfolioDocument")]
        public string PortfolioDocument { get; set; }

        [DataMember(Name = "portfolioCode")]
        public string PortfolioCode { get; set; }

        [DataMember(Name = "portfolioBank")]
        public string PortfolioBank { get; set; }

        [DataMember(Name = "portfolioBranch")]
        public string PortfolioBranch { get; set; }

        [DataMember(Name = "portfolioAccount")]
        public string PortfolioAccount { get; set; }

        [DataMember(Name = "portfolioCetipAccount")]
        public string PortfolioCetipAccount { get; set; }

        // Dados da Operação

        [DataMember(Name = "operationTypeId")]
        public short OperationTypeId { get; set; }

        [DataMember(Name = "amount")]
        public decimal Amount { get; set; }

        [DataMember(Name = "unitPrice")]
        public decimal UnitPrice { get; set; }

        [DataMember(Name = "isTerm")]
        public bool IsTerm { get; set; }

        [DataMember(Name = "acquisitionDate")]
        public DateTimeOffset? AcquisitionDate { get; set; }

        [DataMember(Name = "isSecondaryMarket")]
        public bool IsSecondaryMarket { get; set; }

        [DataMember(Name = "operationValue")]
        public decimal OperationValue { get; set; }

        [DataMember(Name = "assetType")]
        public string AssetType { get; set; }

        [DataMember(Name = "assetCode")]
        public string AssetCode { get; set; }

        [DataMember(Name = "expirationDate")]
        public DateTimeOffset ExpirationDate { get; set; }

        [DataMember(Name = "issueFee")]
        public decimal IssueFee { get; set; }

        [DataMember(Name = "issueDate")]
        public DateTimeOffset IssueDate { get; set; }

        [DataMember(Name = "counterpartName")]
        public string CounterpartName { get; set; }

        [DataMember(Name = "counterpartDocument")]
        public string CounterpartDocument { get; set; }

        [DataMember(Name = "counterpartCetipAccount")]
        public string CounterpartCetipAccount { get; set; }

        [DataMember(Name = "command")]
        public string Command { get; set; }

        [DataMember(Name = "annotations")]
        public string Annotations { get; set; }

        [DataMember(Name = "objectAction")]
        public string ObjectAction { get; set; }

        [DataMember(Name = "accountManagerName")]
        public string AccountManagerName { get; set; }

        [DataMember(Name = "accountManagerDocument")]
        public string AccountManagerDocument { get; set; }

        [DataMember(Name = "index")]
        public string Index { get; set; }

        [DataMember(Name = "indexPercent")]
        public decimal IndexPercent { get; set; }

        [DataMember(Name = "issuer")]
        public string Issuer { get; set; }

        [DataMember(Name = "indexBase")]
        public decimal IndexBase { get; set; }

        [DataMember(Name = "interestType")]
        public decimal InterestType { get; set; }
    }
}

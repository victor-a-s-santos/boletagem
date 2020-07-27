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
    public partial class TicketVariableIncomeItemModel
    {
        [DataMember(Name = "Id")]
        public int? Id { get; set; }

        [DataMember(Name = "IdTicket")]
        public int? IdTicket { get; set; }

        [DataMember(Name = "TradingCode")]
        public string TradingCode { get; set; }

        [DataMember(Name = "TradingCodeBusinessCode")]
        public string TradingCodeBusinessCode { get; set; }

        [DataMember(Name = "BuySell")]
        public string BuySell { get; set; }

        [DataMember(Name = "MarketType")]
        public string MarketType { get; set; }

        [DataMember(Name = "Amount")]
        public int? Amount { get; set; }

        [DataMember(Name = "SettlementTypeOfSecondaryTerm")]
        public string SettlementTypeOfSecondaryTerm { get; set; }

        [DataMember(Name = "Price")]
        public decimal Price { get; set; }

        [DataMember(Name = "Factor")]
        public int? Factor { get; set; }

        [DataMember(Name = "SettlementType")]
        public string SettlementType { get; set; }

        [DataMember(Name = "AssetCode")]
        public string AssetCode { get; set; }

        [DataMember(Name = "ISINCode")]
        public string ISINCode { get; set; }

        [DataMember(Name = "ISINCodeDistribution")]
        public string ISINCodeDistribution { get; set; }

        [DataMember(Name = "CompanyName")]
        public string CompanyName { get; set; }

        [DataMember(Name = "Specification")]
        public string Specification { get; set; }

        [DataMember(Name = "SpecificationIndicator")]
        public string SpecificationIndicator { get; set; }

        [DataMember(Name = "IsAfterMarket")]
        public string IsAfterMarket { get; set; }

        [DataMember(Name = "NewItem")]
        public bool? NewItem { get; set; }
    }
}
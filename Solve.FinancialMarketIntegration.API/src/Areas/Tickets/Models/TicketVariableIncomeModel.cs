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
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
    [DataContract]
    public partial class TicketVariableIncomeModel : TicketModel
    {
        [DataMember(Name = "BrokerCode")]
        public string BrokerCode { get; set; }

        [DataMember(Name = "StockExchangeDate")]
        public DateTimeOffset? StockExchangeDate { get; set; }

        [DataMember(Name = "ClientCode")]
        public string ClientCode { get; set; }

        [DataMember(Name = "ClientCodeDigit")]
        public string ClientCodeDigit { get; set; }

        [DataMember(Name = "SellTotal")]
        public decimal? SellTotal { get; set; }
        [DataMember(Name = "BuyTotal")]
        public decimal? BuyTotal { get; set; }

        [DataMember(Name = "Items")]
        public List<TicketVariableIncomeItemModel> Items { get; set; }

        [DataMember(Name = "PagedResultItems")]
        public PagedResult<TicketVariableIncomeItemModel> PagedResultItems { get; set; }

        [DataMember(Name = "accountManagerName")]
        public string AccountManagerName { get; set; }

        [DataMember(Name = "accountManagerDocument")]
        public string AccountManagerDocument { get; set; }

        [DataMember(Name = "deleted")]
        public IEnumerable<int> Deleted { get; set; }

        public IEnumerable<TicketVariableIncomeItemModel> GetPendingInsertion() {
            foreach (var item in Items)
            {
                if (item.NewItem.HasValue && item.NewItem.Value) {
                    yield return item;
                }
            }
        }

        public IEnumerable<TicketVariableIncomeItemModel> GetExisting() {
            foreach (var item in Items)
            {
                if (!item.NewItem.HasValue || !item.NewItem.Value) {
                    yield return item;
                }
            }
        }
    }
}
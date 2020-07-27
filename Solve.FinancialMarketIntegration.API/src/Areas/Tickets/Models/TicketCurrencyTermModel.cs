using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
   public class TicketCurrencyTermModel: ITicketModel
    {
        public int? Id { get; set; }
        public PortfolioModel Portfolio { get; set; }
        public DateTimeOffset? OperationDate { get; set; }
        public decimal OperationValue { get; set; }
        public short OperationTypeId { get; set; }
        public int? StatusId { get; set; }
        public string StatusDescription { get; set; }
        public IEnumerable<int> StatusRequiredRoles { get; internal set; }

        public DateTimeOffset ExpirationDate { get; set; }

        public bool CetipSettlement { get; set; }

        public string ContractNumber { get; set; }

        public decimal FutureFee { get; set; }

        public int QuoteExpirationTypeId { get; set; }

        public int CurrencyId { get; set; }

        public bool CrossRate { get; set; }

        public CounterpartDTO Counterpart { get; set; }

        public string Annotations { get; set; }

        public DateTimeOffset? WorkflowStartDate { get; set; }

        public DateTimeOffset? WorkflowEndDate { get; set; }

        public string AccountManagerName { get; set; }


        public string AccountManagerDocument { get; set; }


        public void MapWorkflowData(IDictionary<int, WorkflowDetails> source, int? workflowId)
        {                                                               
            if (!(workflowId.HasValue && source.TryGetValue(workflowId.Value, out WorkflowDetails workflowDetails))) return;
            MapWorkflowData(workflowDetails);
        }

        public void MapWorkflowData(WorkflowDetails source)
        {
            if (source == null) return;

            StatusId = source.StatusId;
            StatusDescription = source.StatusDescription;
            StatusRequiredRoles = source.StatusRequiredRoles;
            WorkflowStartDate = source.StartDate;
            WorkflowEndDate = source.EndDate;
        }
    }
}

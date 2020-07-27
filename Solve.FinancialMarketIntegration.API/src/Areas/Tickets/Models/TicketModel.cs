using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Models
{
    [DataContract]
    public abstract class TicketModel : ITicketModel
    {
        [DataMember(Name = "id", IsRequired = false)]
        public int? Id { get; set; }

        [DataMember(Name = "operationDate")]
        public DateTimeOffset? OperationDate { get; set; }

        [DataMember(Name = "statusId")]
        public int? StatusId { get; set; }

        [DataMember(Name = "statusDescription")]
        public string StatusDescription { get; set; }

        [DataMember(Name = "workflowStartDate")]
        public DateTimeOffset? WorkflowStartDate { get; set; }

        [DataMember(Name = "workflowEndDate")]
        public DateTimeOffset? WorkflowEndDate { get; set; }

        [DataMember(Name = "statusRequiredRoles")]
        public IEnumerable<int> StatusRequiredRoles { get; internal set; }

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

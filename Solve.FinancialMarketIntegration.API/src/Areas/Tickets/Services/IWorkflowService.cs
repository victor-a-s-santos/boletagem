
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Services
{
    public interface IWorkflowService
    {
        /// <summary>
        /// Open a new workflow instance
        /// </summary>
        /// <param name="type">Workflow Type</param>
        /// <param name="userId">User Id</param>
        /// <returns>
        /// Returns workflow instance id
        /// </returns>
        Task<int> OpenAsync(int type, string userId);

        /// <summary>
        /// Open a new workflow instance in a step with specific status
        /// </summary>
        /// <param name="type">Workflow Type</param>
        /// <param name="userId">User Id</param>
        /// <param name="status">Step's status</param>
        /// <returns>
        /// Returns workflow instance id
        /// </returns>
        Task<int> OpenAsync(int type, string userId, int? status);

        /// <summary>
        /// Approve current workflow status
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="username"></param>
        /// <param name="comments"></param>
        /// <returns>
        /// Returns true if workflow is finished
        /// </returns>
        Task<bool> ApproveAsync(int workflowId, string userId, string comments = "");

        /// <summary>
        /// Reject current workflow status
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="username"></param>
        /// <param name="comments"></param>
        /// <returns>
        /// Returns true if workflow is finished
        /// </returns>
        Task<bool> RejectAsync(int workflowId, string userId, string comments = "");

        Task<int> CancelDraftAsync(int type, string userId);

        //Task<WorkflowHistory> GetHistoryAsync(int workflowId, string userId);

        Task<WorkflowDetails> GetWorkflowDetailsAsync(int workflowId);

        Task<IEnumerable<WorkflowDetails>> GetWorkflowsDetailAsync(IEnumerable<int> workflowIds);
        IEnumerable<WorkflowDetails> GetWorkflowsDetail(IEnumerable<int> workflowIds);
        IEnumerable<WorkflowDetails> GetWorkflowsBy(int statusId, DateTimeOffset startDate, DateTimeOffset endDate);
    }

    //public class Step
    //{
    //    public int WorkflowId { get; set; }
    //    public int StatusId { get; set; }
    //    public bool CanProceed { get; set; }
    //    public string Description { get; set; }
    //    public IEnumerable<int> RequiredRoles { get; internal set; }
    //}


    //public class WorkflowHistory
    //{

    //    public WorkflowHistory()
    //    {
    //        Steps = new List<StepHistory>();
    //    }

    //    public int WorkflowId { get; set; }
    //    public DateTimeOffset? WorkflowStartDate { get; set; }
    //    public string AuthorUserId { get; set; }
    //    public string AuthorUserName { get; set; }
    //    public IEnumerable<StepHistory> Steps { get; set; }
    //}

    //public class StepHistory
    //{
    //    public int? StatusId { get; set; }
    //    public string Description { get; set; }
    //    public IEnumerable<ApprovalHistory> Results { get; set; }
    //    public NextStep NextStep { get; set; }
    //    public bool IsFirstStep { get; set; }
    //}

    //public class ApprovalHistory
    //{
    //    public int? ResultId { get; set; }
    //    public bool? Approved { get; set; }
    //    public string Justificative { get; set; }
    //    public string User { get; set; }
    //    public string UserName { get; set; }
    //    public DateTimeOffset? Date { get; set; }
    //}

    //public class NextStep
    //{
    //    public int? StatusId { get; set; }
    //    public string Description { get; set; }
    //}

    public class WorkflowDetails
    {
        public int WorkflowId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public int? StatusId { get; set; }
        public string StatusDescription { get; set; }
        public IEnumerable<int> StatusRequiredRoles { get; set; }
        public bool CanProceed { get; internal set; }
    }
}
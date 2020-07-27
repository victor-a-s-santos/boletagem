using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Entities
{
    public class Workflow : Entity
    {
        public Workflow()
        {
            Steps = new List<WorkflowStepResult>();
            StartDate = DateTimeOffset.Now;
        }
        public WorkflowStepResult CurrentStep { get; set; }

        public bool IsFinished { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }
        public ICollection<WorkflowStepResult> Steps { get; set; }

        /// <summary>
        /// Approve current workflow status
        /// </summary>
        /// <param name="username"></param>
        /// <param name="comments"></param>
        /// <returns>
        /// Returns true if workflow is finished
        /// </returns>
        public bool Approve(string username, string comments) => ChangeStatus(username, comments, isApproved: true);

        /// <summary>
        /// Reject current workflow status
        /// </summary>
        /// <param name="username"></param>
        /// <param name="comments"></param>
        /// <returns>
        /// Returns true if workflow is finished
        /// </returns>
        public bool Reject(string username, string comments) => ChangeStatus(username, comments, isApproved: false);

        private bool ChangeStatus(string username, string comments, bool isApproved)
        {
            var approval = CurrentStep.Approvals.FirstOrDefault(a => a.IsApproved == null);
            if (approval == null)
                return false;

            approval.IsApproved = isApproved;
            approval.Comments = comments;

            if (!CurrentStep.Approvals.All(a => a.IsApproved.HasValue))
                return false;

            var nextStep = isApproved ? CurrentStep.NextStepApproved : CurrentStep.NextStepRejected;
            if (nextStep == null)
                throw new InvalidOperationException("Workflow is already finished!");

            if (!nextStep.HasNextStep())
                Finish();

            CurrentStep = nextStep;

            return IsFinished;
        }

        public void Finish()
        {
            IsFinished = true;
            EndDate = DateTimeOffset.Now;
        }
    }
}

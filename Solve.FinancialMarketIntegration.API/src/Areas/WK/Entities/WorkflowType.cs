using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Entities
{
    public class WorkflowType : Entity
    {
        public WorkflowType()
        {
            Steps = new List<WorkflowApprovalStep>();
            Workflows = new List<Workflow>();
        }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<WorkflowApprovalStep> Steps { get; set; }
        public ICollection<Workflow> Workflows { get; set; }

        public Workflow CreateNewInstance()
        {
            var workflow = new Workflow { };

            Workflows.Add(workflow);

            return workflow;
        }

        public void Configure(Workflow workflow, int? status)
        {
            CreateSteps(workflow, status);
            ConfigureSteps(workflow);
        }

        private void CreateSteps(Workflow workflow, int? status)
        {
            foreach (var step in Steps)
            {
                var workflowStep = step.CreateNewInstance();

                if (workflow.CurrentStep != null)
                {
                    workflow.Steps.Add(workflowStep);
                    continue;
                }

                if (status.HasValue)
                {
                    if (workflowStep.WorkflowStatus.Id == status)
                    {
                        workflow.CurrentStep = workflowStep;

                        if (!workflowStep.HasNextStep()){
                            workflow.Finish();
                        }
                    }

                    workflow.Steps.Add(workflowStep);
                    continue;
                }

                if (workflowStep.IsFirstStep)
                    workflow.CurrentStep = workflowStep;

                workflow.Steps.Add(workflowStep);
            }
        }

        private void ConfigureSteps(Workflow workflow)
        {
            foreach (var step in Steps)
            {
                var workflowStep = workflow.Steps.Single(s => s.Origin.Id == step.Id);

                if (step.NextStepRejected != null)
                    workflowStep.NextStepRejected = workflow.Steps.First(s => s.Origin.Id == step.NextStepRejected.Id);

                if (step.NextStepApproved != null)
                    workflowStep.NextStepApproved = workflow.Steps.First(s => s.Origin.Id == step.NextStepApproved.Id);

                if (step.IsFirstStep)
                    workflowStep.IsFirstStep = step.IsFirstStep;
            }
        }
    }
}

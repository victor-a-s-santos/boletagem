using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;
using Solve.FinancialMarketIntegration.API.Areas.Security.Services;
using Solve.FinancialMarketIntegration.API.Areas.WK.Controllers;
using Solve.FinancialMarketIntegration.API.Areas.WK.Entities;
using Solve.FinancialMarketIntegration.API.Areas.WK.Events;
using Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.Cache;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;

namespace Solve.FinancialMarketIntegration.API.Tests.Areas.WK
{
    public abstract class ChangeWorkflowStatusTests : WorkflowContextTests
    {
        protected IWorkflowService Service { get; set; }
        protected Workflow Workflow { get; set; }

        protected WorkflowStepResult ApprovedStep { get; set; }
        protected WorkflowStepResult WaitingStep { get; set; }
        protected WorkflowStepResult RejectedStep { get; set; }
        protected WorkflowStepResult FirstStep { get; set; }

        [SetUp]
        protected new void Setup()
        {
            Workflow = new Workflow { };
            Type.Workflows.Add(Workflow);


            ApprovedStep = new WorkflowStepResult { };
            WaitingStep = new WorkflowStepResult { NextStepApproved = ApprovedStep };
            RejectedStep = new WorkflowStepResult { };
            FirstStep = new WorkflowStepResult { IsFirstStep = true, NextStepApproved = WaitingStep, NextStepRejected = RejectedStep };

            FirstStep.Approvals.Add(new WorkflowApprovalStepResult { RoleId = 1 });
            WaitingStep.Approvals.Add(new WorkflowApprovalStepResult { RoleId = 1 });
            ApprovedStep.Approvals.Add(new WorkflowApprovalStepResult { RoleId = 1 });

            Workflow.Steps.Add(FirstStep);
            Workflow.CurrentStep = FirstStep;

            Context.Workflows.Add(Workflow);
            Context.SaveChanges();

            Service = new WorkflowController(Context, mockAuthService.Object, workflowCache, eventBus);
        }
    }
}

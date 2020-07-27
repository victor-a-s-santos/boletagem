using Moq;
using NUnit.Framework;
using Solve.FinancialMarketIntegration.API.Areas.Security.Services;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using Solve.FinancialMarketIntegration.API.Areas.WK.Controllers;
using Solve.FinancialMarketIntegration.API.Areas.WK.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solve.FinancialMarketIntegration.API.Tests.Areas.WK
{
    /// <summary>
    /// Approve workflows tests
    /// </summary>
    public class ApproveWorkflowStepTests : ChangeWorkflowStatusTests
    {
        [Test]
        public void ApproveFirstStep()
        {
            var isFinished = Service.ApproveAsync(Workflow.Id, "admin").WaitAndGetResult();
            
            Assert.IsFalse(isFinished);
            Assert.AreEqual(Workflow.CurrentStep, WaitingStep);
        }

        [Test]
        public void ApproveLastStep()
        {
            var isFinishedFirstStep = Service.ApproveAsync(Workflow.Id, "admin").WaitAndGetResult();
            var isFinishedLastStep = Service.ApproveAsync(Workflow.Id, "admin").WaitAndGetResult();

            Assert.IsFalse(isFinishedFirstStep);
            Assert.IsTrue(isFinishedLastStep);
            Assert.AreEqual(Workflow.CurrentStep, ApprovedStep);
        }
    }
}

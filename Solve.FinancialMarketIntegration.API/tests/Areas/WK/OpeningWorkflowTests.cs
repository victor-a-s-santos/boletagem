using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using Solve.FinancialMarketIntegration.API.Areas.WK.Controllers;
using Solve.FinancialMarketIntegration.API.Areas.WK.Entities;

namespace Solve.FinancialMarketIntegration.API.Tests.Areas.WK
{
    /// <summary>
    /// Tests about opening a new workflow
    /// </summary>
    public class OpeningWorkflowTests : WorkflowContextTests
    {
        private IWorkflowService service;

        [SetUp]
        public void Setup()
        {
            service = new WorkflowController(Context, mockAuthService.Object, workflowCache, eventBus);
        }

        [Test]
        public void OpenExistingWorkflow()
        {
            var workflowId = service.OpenAsync(Type.Id, "admin").WaitAndGetResult();

            var workflow = Context.Workflows.Include(w => w.Steps)
                                            .FirstOrDefaultAsync(w => w.Id == workflowId)
                                            .WaitAndGetResult();

            Assert.IsNotNull(workflow);
            Assert.AreEqual(Type.Steps.Count, workflow.Steps.Count);
        }

        [Test]
        public void OpenWorkflowInASpecificStatus()
        {
            var workflowId = service.OpenAsync(Type.Id, "admin", approvedStatus.Id).WaitAndGetResult();

            var workflow = Context.Workflows.Include(w => w.CurrentStep)
                                            .ThenInclude(s => s.WorkflowStatus)
                                            .FirstOrDefaultAsync(w => w.Id == workflowId)
                                            .WaitAndGetResult();

            Assert.IsNotNull(workflow);
            Assert.AreEqual(workflow.CurrentStep.WorkflowStatus.Id, approvedStatus.Id);
        }

        [Test]
        public void OpenNonEextingWorkflow()
        {
            var workflowId = service.OpenAsync(-100000, "admin").WaitAndGetResult();
            Assert.AreEqual(workflowId, 0);
        }
    }
}
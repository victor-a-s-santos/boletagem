using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Solve.FinancialMarketIntegration.API.Areas.Security.Services;
using Solve.FinancialMarketIntegration.API.Areas.WK.Entities;
using Solve.FinancialMarketIntegration.API.Areas.WK.Events;
using Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.Cache;
using Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Solve.FinancialMarketIntegration.API.Tests.Areas.WK
{
    public abstract class WorkflowContextTests
    {
        protected WorkflowDataContext Context;
        protected WorkflowType Type;
        protected WorkflowStatus waitingApprovalStatus;
        protected WorkflowStatus approvedStatus;
        protected WorkflowStatus rejectedStatus;
        protected WorkflowCache workflowCache;
        protected Mock<IServiceProvider> mockServiceProvider;
        protected EventBus eventBus;
        protected Mock<IAuthService> mockAuthService;

        [SetUp]
        protected void Setup()
        {
            var options = new DbContextOptionsBuilder<WorkflowDataContext>()
                .UseInMemoryDatabase(databaseName: "workflow-tets")
                .Options;

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
            }, "mock"));

            var context = new DefaultHttpContext { User = user };
            mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

            Context = new WorkflowDataContext(options, mockHttpContextAccessor.Object);

            waitingApprovalStatus = new WorkflowStatus { Name = "Waiting Approval" };
            approvedStatus = new WorkflowStatus { Name = "Approved" };
            rejectedStatus = new WorkflowStatus { Name = "Reject" };

            Type = new WorkflowType { Name = "Workflow", IsActive = true };
            var approvedStep = new WorkflowApprovalStep { IsActive = true, WorkflowStatus = approvedStatus };
            var rejectedStep = new WorkflowApprovalStep { IsActive = true, WorkflowStatus = rejectedStatus };
            var firstStep = new WorkflowApprovalStep
            {
                NextStepApproved = approvedStep,
                NextStepRejected = rejectedStep,
                WorkflowStatus = waitingApprovalStatus,
                IsActive = true,
                IsFirstStep = true
            };

            Type.Steps.Add(firstStep);
            Type.Steps.Add(approvedStep);
            Type.Steps.Add(rejectedStep);

            Context.WorkflowTypes.Add(Type);
            Context.SaveChanges();

            mockAuthService = new Mock<IAuthService>();
            mockAuthService.Setup(_ => _.HasRoles(It.IsAny<string>(), It.IsAny<IEnumerable<int>>())).Returns(true);

            var cache = new MemoryCache(new MemoryCacheOptions());
            workflowCache = new WorkflowCache(cache, Context);



            eventBus = new EventBus(new MockServiceProvider());
        }
    }

    public class MockServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            return new MockEventHandler[] { };
        }
    }

    public class MockEvent : IEvent
    {
    }

    public class MockEventHandler : IEventHandler<MockEvent>
    {
        public void Execute(MockEvent e)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.WK.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess.Configurations
{
    public class WorkflowStepResultConfiguration : IEntityTypeConfiguration<WorkflowStepResult>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkflowStepResult> builder)
        {
            builder.ToTable("StepResult", "Workflow");

            builder.HasOne(t => t.NextStepApproved)
            .WithMany()
            .HasForeignKey("NextStepApprovedId");

            builder.HasOne(t => t.NextStepRejected)
            .WithMany()
            .HasForeignKey("NextStepRejectedId");

            builder.HasOne(t => t.Origin)
            .WithMany()
            .HasForeignKey("StepOriginId")
            .IsRequired();
        }
    }
}

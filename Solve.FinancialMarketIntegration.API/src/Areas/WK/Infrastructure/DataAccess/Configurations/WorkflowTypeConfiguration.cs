using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.WK.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess.Configurations
{
    public class WorkflowTypeConfiguration : IEntityTypeConfiguration<WorkflowType>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkflowType> builder)
        {
            builder.ToTable("Type", "Workflow");

            builder.HasMany(t => t.Workflows)
            .WithOne()
            .IsRequired();
        }
    }
}

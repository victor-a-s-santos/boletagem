using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.WK.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.DataAccess.Configurations
{
    public class WorkflowApprovalRoleConfiguration : IEntityTypeConfiguration<WorkflowApprovalRole>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkflowApprovalRole> builder)
        {
            builder.ToTable("ApprovalRole", "Workflow");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Group", "Security");

            builder.Property(t => t.Name)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.HasIndex(t => t.Name)
                   .IsUnique();
        }
    }
}

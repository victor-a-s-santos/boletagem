using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class FundConfiguration : IEntityTypeConfiguration<Fund>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Fund> builder)
        {
            builder.ToTable("Fund", "Ticket");

            builder.Property(i => i.Name).HasColumnName("FundName")
                                .HasMaxLength(80)
                                .IsRequired(true)
                                .IsUnicode(false);

            builder.Property(i => i.Document).HasColumnName("FundDocument")
                .HasMaxLength(14)
                .IsRequired(true)
                .IsUnicode(false);

            builder.Property(i => i.Code).HasColumnName("FundCode")
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false);

            builder.Property(i => i.CetipAccount).HasColumnName("FundCetipAccount")
                .HasMaxLength(8)
                .IsRequired(true)
                .IsUnicode(false);

            builder.Property(i => i.IssueDate).HasColumnName("IssueDate");

            builder.Property(i => i.IsFIDC).HasColumnName("IsFIDC");

            builder.Property(i => i.IsNewIssue).HasColumnName("IsNewIssue");

            builder.Property(i => i.Class).HasColumnName("FundClass")
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false);

            builder.Property(i => i.CodeIssuer).HasColumnName("FundCodeIssuer")
                .HasMaxLength(10)
                .IsRequired(true)
                .IsUnicode(false);

            builder.Property(i => i.Type).HasColumnName("FundType")
                .HasColumnType("char(3)")
                .IsRequired(true)
                .IsUnicode(false);
        }
    }
}

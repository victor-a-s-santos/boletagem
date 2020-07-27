using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class TicketPublicFixedIncomeConfiguration : IEntityTypeConfiguration<TicketPublicFixedIncome>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TicketPublicFixedIncome> builder)
        {
            builder.ToTable("TicketPublicFixedIncome", "Ticket");

            builder.HasKey("TicketId");

            builder.HasOne(t => t.Ticket)
                .WithOne();

            builder.HasOne(t => t.OperationType)
                .WithMany()
                .IsRequired(true)
                .HasForeignKey("OperationTypeId");

            builder.Property(t => t.UnitPrice)
                .HasColumnType("decimal(18,8)")
                .IsRequired(true);

            builder.Property(t => t.Amount)
                .HasColumnType("bigint");

            builder.Property(t => t.OperationValue)
                .HasColumnType("decimal(28,8)")
                .IsRequired(true);

            builder.Property(i => i.Security).HasColumnName("Security")
                    .HasMaxLength(80)
                    .IsRequired(true)
                    .IsUnicode(false);

            builder.Property(i => i.SecurityId).HasColumnName("SecurityId")
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);

            builder.Property(t => t.IssueTax)
                .HasColumnType("decimal(18,8)")
                .IsRequired(false);

            builder.OwnsOne(t => t.Counterpart, p =>
            {
                p.Property(i => i.Name).HasColumnName("CounterpartName")
                    .HasMaxLength(90)
                    .IsRequired(true)
                    .IsUnicode(false);

                p.Property(i => i.Document).HasColumnName("CounterpartDocument")
                    .HasMaxLength(14)
                    .IsRequired(true)
                    .IsUnicode(false);

                p.Property(i => i.ClearingAccount).HasColumnName("CounterpartClearingAccount")
                    .HasMaxLength(10)
                    .IsRequired(true)
                    .IsUnicode(false);

                p.Property(i => i.Command).HasColumnName("CounterpartCommand")
                    .HasMaxLength(10)
                    .HasDefaultValue("0")
                    .IsRequired(false)
                    .IsUnicode(false);
            });
        }
    }
}

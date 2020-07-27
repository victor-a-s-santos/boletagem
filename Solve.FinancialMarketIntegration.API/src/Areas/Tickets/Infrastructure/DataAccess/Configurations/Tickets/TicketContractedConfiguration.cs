using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class TicketContractedConfiguration : IEntityTypeConfiguration<TicketContracted>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TicketContracted> builder)
        {
            builder.ToTable("TicketContracted", "Ticket");

            builder.HasKey("TicketId");

            builder.HasOne(t => t.Ticket)
                .WithOne();

            builder.HasOne(t => t.OperationType)
                .WithMany()
                .IsRequired(true)
                .HasForeignKey("OperationTypeId");

            builder.Property(t => t.UnitPriceOutward)
                .HasColumnType("decimal(18,8)")
                .IsRequired(true);

            builder.Property(t => t.UnitPriceReturn)
                .HasColumnType("decimal(18,8)")
                .IsRequired(true);

            builder.Property(t => t.ValueOutward)
                .HasColumnType("decimal(18,8)")
                .IsRequired(true);

            builder.Property(t => t.ValueReturn)
                .HasColumnType("decimal(18,8)")
                .IsRequired(true);

            builder.Property(t => t.OperationValue)
                .HasColumnType("decimal(28,8)")
                .IsRequired(true);

            builder.Property(t => t.Amount)
                .HasColumnType("bigint")
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
                .HasColumnType("decimal(12,8)")
                .IsRequired(true);

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
                    .HasMaxLength(15)
                    .IsRequired(true)
                    .IsUnicode(false);

                p.Property(i => i.Command).HasColumnName("CounterpartCommand")
                    .HasMaxLength(10)
                    .HasDefaultValue("0")
                    .IsRequired(true)
                    .IsUnicode(false);
            });
        }
    }
}

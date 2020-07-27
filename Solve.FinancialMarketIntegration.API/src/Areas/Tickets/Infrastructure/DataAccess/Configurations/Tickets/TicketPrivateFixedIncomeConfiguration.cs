using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class TicketPrivateFixedIncomeConfiguration : IEntityTypeConfiguration<TicketPrivateFixedIncome>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TicketPrivateFixedIncome> b)
        {
            b.ToTable("TicketPrivateFixedIncome", "Ticket");

            b.HasKey("TicketId");

            b.HasOne(t => t.Ticket)
                .WithOne();

            b.HasOne(t => t.OperationType)
                .WithMany()
                .IsRequired(true)
                .HasForeignKey("OperationTypeId");

            b.Property(t => t.UnitPrice)
                .IsRequired(true)
                .HasColumnType("decimal(18,8)");

            b.Property(t => t.OperationValue)
                .IsRequired(true)
                .HasColumnType("decimal(28,8)");

            b.Property(w => w.Amount)
                .IsRequired(true)
                .HasColumnType("decimal(18,8)");

            b.Property(w => w.IssueFee)
                .IsRequired(true)
                .HasColumnType("decimal(9,6)");

            b.Property(w => w.Issuer)
                .IsRequired(false)
                .HasMaxLength(90);

            b.Property(t => t.AssetType)
                .IsRequired(true)
                .HasMaxLength(20);

            b.Property(t => t.AssetCode)
                .HasMaxLength(15);

            b.Property(t => t.ObjectAction)
                .IsRequired(false);

            b.Property(t => t.AcquisitionDate)
                .IsRequired(false);

            b.OwnsOne(t => t.Counterpart, p =>
            {
                p.Property(i => i.Name).HasColumnName("CounterpartName")
                    .HasMaxLength(90)
                    .IsRequired(false)
                    .IsUnicode(false);

                p.Property(i => i.Document).HasColumnName("CounterpartDocument")
                    .HasMaxLength(14)
                    .IsRequired(false)
                    .IsUnicode(false);

                p.Property(i => i.ClearingAccount).HasColumnName("CounterpartClearingAccount")
                    .HasMaxLength(10)
                    .IsRequired(false)
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

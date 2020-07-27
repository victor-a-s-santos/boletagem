using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations.Tickets
{
    /// <summary>
    /// Termo Moeda
    /// </summary>
    public class CurrencyTermConfiguration : IEntityTypeConfiguration<TicketCurrencyTerm>
    {
        public void Configure(EntityTypeBuilder<TicketCurrencyTerm> b)
        {
            b.ToTable("TicketCurrencyTerm", "Ticket");

            b.HasKey("TicketId");

            b.HasOne(t => t.Ticket)
                .WithOne();

            b.HasOne(t => t.OperationType)
                .WithMany()
                .IsRequired(false)
                .HasForeignKey("OperationTypeId");

            b.Property(p => p.FutureFee).HasColumnType(_configConst.Taxa);
            b.Property(p => p.OperationValue).HasColumnType("decimal(28,8)");

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

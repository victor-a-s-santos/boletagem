using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class TicketSwapCetipConfiguration : IEntityTypeConfiguration<TicketSwapCetip>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TicketSwapCetip> builder)
        {
            builder.ToTable("TicketSwapCetip", "Ticket");

            builder.HasKey("TicketId");

            builder.HasOne(t => t.Ticket)
                .WithOne();

            builder.Property(t => t.OperationValue)
                .HasColumnType("decimal(28,8)")
                .IsRequired(true);

            builder.Property(t => t.ActiveIndexPercent)
                .HasColumnType("decimal(18,8)")
                .IsRequired(true);

            builder.Property(t => t.ActiveIndexTax)
                .HasColumnType("decimal(18,8)")
                .IsRequired(true);

            builder.Property(t => t.PassiveIndexPercent)
                .HasColumnType("decimal(18,8)")
                .IsRequired(true);

            builder.Property(t => t.PassiveIndexTax)
                .HasColumnType("decimal(18,8)")
                .IsRequired(true);

            builder.OwnsOne(t => t.Counterpart, p =>
            {
                p.Property(i => i.Name).HasColumnName("CounterpartName")
                    .HasMaxLength(90)
                    .IsRequired(true)
                    .IsUnicode(false);

                p.Property(i => i.Document).HasColumnName("CounterpartDocument")
                    .HasMaxLength(14)
                    .IsRequired(false)
                    .IsUnicode(false);

                p.Property(i => i.ClearingAccount).HasColumnName("CounterpartClearingAccount")
                    .HasMaxLength(15)
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

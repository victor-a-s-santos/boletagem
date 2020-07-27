using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class TicketFuturesConfiguration : IEntityTypeConfiguration<TicketFutures>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TicketFutures> builder)
        {
            builder.ToTable("TicketFutures", "Ticket");

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
                .HasColumnType("bigint")
                .IsRequired(true);

            builder.Property(t => t.TradingDate)
                .IsRequired(true);

            builder.Property(t => t.PercentageDiscount)
                .IsRequired(true);

            builder.Property(t => t.PaperCode)
                .IsRequired(true);

            builder.Property(t => t.PaperSerie)
                .HasMaxLength(10)
                .IsRequired(true);

            builder.Property(t => t.BrokerCode)
                .IsRequired(true);

            builder.Property(t => t.BrokerAccount)
                .IsRequired(true);

            builder.Property(t => t.BrokerDocument)
                .IsRequired(true);
        }
    }
}

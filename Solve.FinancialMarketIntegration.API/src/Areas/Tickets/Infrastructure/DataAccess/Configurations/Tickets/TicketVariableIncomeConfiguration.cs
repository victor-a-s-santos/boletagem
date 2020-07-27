using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class TicketVariableIncomeConfiguration : IEntityTypeConfiguration<TicketVariableIncome>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TicketVariableIncome> builder)
        {
            builder.ToTable("TicketVariableIncome", "Ticket");

            builder.HasKey("TicketId");
            builder.HasOne(t => t.Ticket)
                .WithOne();

            builder.Property(t => t.BuyTotal)
                .HasColumnType("decimal(18,8)")
                .IsRequired(true);

            builder.Property(t => t.SellTotal)
                .HasColumnType("decimal(18,8)")
                .IsRequired(true);

            builder
                .HasMany(t => t.LineItems)
                .WithOne(ti => ti.TicketVariableIncome)
                .HasForeignKey(ti => ti.TicketId);

        }
    }
}

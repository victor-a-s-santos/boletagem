using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations.Tickets
{
    public class TicketHistoryConfiguration : IEntityTypeConfiguration<TicketHistory>
    {
        public void Configure(EntityTypeBuilder<TicketHistory> builder)
        {
            builder.ToTable("History", "Ticket");

            builder.Property(t => t.Comments)
                    .IsUnicode(false);

            builder.Property(t => t.Details)
                   .IsUnicode(false);

            builder.HasOne(t => t.Event)
                    .WithMany()
                    .IsRequired();
        }
    }
}

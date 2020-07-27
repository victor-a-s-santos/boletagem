using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class TicketSignoffRequestConfiguration : IEntityTypeConfiguration<TicketSignoffRequest>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TicketSignoffRequest> builder)
        {
            builder.ToTable("TicketSignoffRequest", "Ticket");

            builder.HasKey("Id");

            builder.HasOne(t => t.TicketSignoff)
              .WithMany()
              .IsRequired(true)
              .HasForeignKey("TicketSignoffId");

            builder.Property(t => t.TimeLimit)
              .HasColumnType("time")
              .IsRequired(true);

            builder.Property(t => t.Justificative)
              .IsRequired(true);
        }
    }
}

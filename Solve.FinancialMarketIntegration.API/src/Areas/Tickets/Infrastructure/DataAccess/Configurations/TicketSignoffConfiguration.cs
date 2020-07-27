using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class TicketSignoffConfiguration : IEntityTypeConfiguration<TicketSignoff>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TicketSignoff> builder)
        {
            builder.ToTable("TicketSignoff", "Ticket");

            builder.HasKey("Id");

            builder.Property(t => t.TicketTypeId)
              .IsRequired(true);

            builder.Property(t => t.Type)
              .IsRequired(true)
              .HasMaxLength(60);

            builder.Property(t => t.TimeLimit)
              .HasColumnType("time")
              .IsRequired(true);
        }
    }
}

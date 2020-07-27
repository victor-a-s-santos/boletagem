using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class MarketTypeConfiguration : IEntityTypeConfiguration<MarketType>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MarketType> builder)
        {
            builder.ToTable("MarketType", "Ticket");

            builder.Property(t => t.Name)
                .IsUnicode(false)
                .HasMaxLength(20);
        }
    }
}

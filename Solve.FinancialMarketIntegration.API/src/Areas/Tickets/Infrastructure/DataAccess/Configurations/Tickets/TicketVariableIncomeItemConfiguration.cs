using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class TicketVariableIncomeItemConfiguration : IEntityTypeConfiguration<TicketVariableIncomeItem>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TicketVariableIncomeItem> builder)
        {
            builder.ToTable("TicketVariableIncomeItems", "Ticket");

            builder.HasKey("Id");

            builder.Property("Id").ValueGeneratedOnAdd();
            
            builder.Property(t => t.Amount)
                .HasColumnType("decimal(18,8)")
                .IsRequired(true);

            builder.Property(t => t.Price)
                .HasColumnType("decimal(18,8)")
                .IsRequired(true);

        }
    }
}

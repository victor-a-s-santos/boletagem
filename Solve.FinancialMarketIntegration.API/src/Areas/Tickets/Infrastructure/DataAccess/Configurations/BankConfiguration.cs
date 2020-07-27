using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class BankConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.ToTable("Bank", "Ticket");

            builder.HasKey("Id");

            builder.Property(x => x.Code)
              .IsRequired(true)
              .HasMaxLength(10);

            builder.Property(x => x.Name)
              .IsRequired(true)
              .HasMaxLength(200);

            builder.Property(x => x.ShortName)
              .IsRequired(true)
              .HasMaxLength(100);
        }
    }
}
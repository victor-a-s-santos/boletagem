using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket", "Ticket");

            builder.Property(t => t.Annotations)
                .IsUnicode(false)
                .HasMaxLength(200);

            builder.HasOne(t => t.Type)
                .WithMany()
                .IsRequired(true)
                .HasForeignKey("TypeId");

            builder.HasOne(t => t.AccountManager)
                .WithMany()
                .IsRequired(true)
                .HasForeignKey("AccountManagerId");

            builder.OwnsOne(t => t.Portfolio, p =>
            {
                p.Property(i => i.Name).HasColumnName("PortfolioName")
                    .HasMaxLength(90)
                    .IsRequired(true)
                    .IsUnicode(false);

                p.Property(i => i.Code).HasColumnName("PortfolioCode")
                    .HasMaxLength(21)
                    .IsRequired(false)
                    .IsUnicode(false);

                p.Property(i => i.Document).HasColumnName("PortfolioDocument")
                    .HasMaxLength(14)
                    .IsRequired(true)
                    .IsUnicode(false);

                p.Property(i => i.Bank).HasColumnName("PortfolioBank")
                    .HasMaxLength(20)
                    .IsRequired(false)
                    .IsUnicode(false);

                p.Property(i => i.Branch).HasColumnName("PortfolioBranch")
                    .HasMaxLength(10)
                    .IsRequired(false)
                    .IsUnicode(false);

                p.Property(i => i.Account).HasColumnName("PortfolioAccount")
                    .HasMaxLength(15)
                    .IsRequired(true)
                    .IsUnicode(false);

                p.Property(i => i.ClearingAccount).HasColumnName("PortfolioClearingAccount")
                    .HasMaxLength(15)
                    .IsRequired(false)
                    .IsUnicode(false);
            });

            builder.HasMany(t => t.History)
                   .WithOne()
                   .IsRequired();
        }
    }
}

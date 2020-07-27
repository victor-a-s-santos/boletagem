using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class TicketFundQuotasConfiguration : IEntityTypeConfiguration<TicketFundQuotas>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TicketFundQuotas> builder)
        {
            builder.ToTable("TicketFundQuotas", "Ticket");

            builder.HasKey("TicketId");

            builder.HasOne(t => t.Ticket)
                .WithOne();

            builder.HasOne(t => t.OperationType)
                .WithMany()
                .IsRequired(true)
                .HasForeignKey("OperationTypeId");

            builder.HasOne(t => t.SettlementType)
                .WithMany()
                .IsRequired(true)
                .HasForeignKey("SettlementTypeId");

            builder.Property(t => t.Amount)
                .HasColumnType("decimal(18,8)")
                .IsRequired(false);

            builder.Property(t => t.QuotaValue)
                .HasColumnType("decimal(18,8)")
                .IsRequired(false);

            builder.Property(t => t.SettlementDate)
                .IsRequired(false);

            builder.Property(t => t.QuotationDate)
                .IsRequired(false);

            builder.Property(t => t.OperationValue)
                .HasColumnType("decimal(28,8)")
                .IsRequired(true);

            builder.Property(t => t.CetipVoiceId)
                .IsRequired(false)
                .HasMaxLength(20);

            builder.OwnsOne(t => t.Fund, p =>
            {
                p.Property(i => i.Name).HasColumnName("FundName")
                    .HasMaxLength(90)
                    .IsRequired(true)
                    .IsUnicode(false);

                p.Property(i => i.Document).HasColumnName("FundDocument")
                    .HasMaxLength(14)
                    .IsRequired(true)
                    .IsUnicode(false);

                p.Property(i => i.IsFIDC).HasColumnName("IsFIDC");

                p.Property(i => i.IsNewFund).HasColumnName("IsNewFund");

                p.Property(i => i.ClassSeries).HasColumnName("FundClassSeries")
                    .HasMaxLength(12)
                    .IsRequired(false)
                    .IsUnicode(false);

                p.Property(i => i.IssuerName).HasColumnName("FundIssuerName")
                    .HasMaxLength(90)
                    .IsRequired(false)
                    .IsUnicode(false);

                p.Property(i => i.Type).HasColumnName("FundType")
                    .HasColumnType("char(3)")
                    .IsRequired(true)
                    .IsUnicode(false);

                p.Property(i => i.Bank).HasColumnName("FundBank")
                    .HasMaxLength(20)
                    .IsRequired(false)
                    .IsUnicode(false);

                p.Property(i => i.Branch).HasColumnName("FundBranch")
                    .HasMaxLength(10)
                    .IsRequired(false)
                    .IsUnicode(false);

                p.Property(i => i.Account).HasColumnName("FundAcccount")
                    .HasMaxLength(15)
                    .IsRequired(false)
                    .IsUnicode(false);       

                p.Property(i => i.MnemonicCode).HasColumnName("FundMnemonicCode")
                    .HasMaxLength(20)
                    .IsRequired(false)
                    .IsUnicode(false);         
            });

            builder.OwnsOne(t => t.Counterpart, p =>
            {
                p.Property(i => i.Name).HasColumnName("CounterpartName")
                    .HasMaxLength(90)
                    .IsRequired(false)
                    .IsUnicode(false);

                p.Property(i => i.Document).HasColumnName("CounterpartDocument")
                    .HasMaxLength(14)
                    .IsRequired(false)
                    .IsUnicode(false);

                p.Property(i => i.ClearingAccount).HasColumnName("CounterpartClearingAccount")
                    .HasMaxLength(10)
                    .IsRequired(false)
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

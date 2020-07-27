using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class TicketMarginConfiguration : IEntityTypeConfiguration<TicketMargin>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TicketMargin> builder)
        {
            builder.ToTable("TicketMargin", "Ticket");

            builder.HasKey("TicketId");

            builder.HasOne(t => t.Ticket)
                .WithOne();

            builder.HasOne(t => t.OperationType)
                .WithMany()
                .IsRequired(true)
                .HasForeignKey("OperationTypeId");

            builder.HasOne(t => t.MarketType)
                .WithMany()
                .IsRequired(true)
                .HasForeignKey("MarketTypeId");

            builder.Property(t => t.Amount)
                .HasColumnType("decimal(18,8)")
                .IsRequired(false);

            builder.Property(t => t.UnitPrice)
                .HasColumnType("decimal(18,8)")
                .IsRequired(false);

            builder.Property(t => t.OperationValue)
                .HasColumnType("decimal(28,8)")
                .IsRequired(false);
            
            builder.Property(t => t.Issuer)
                .IsRequired(false)
                .HasMaxLength(90);

            builder.Property(t => t.Quotation)
                .HasColumnType("decimal(18,8)")
                .IsRequired(false);

            builder.Property(t => t.CounterpartBrokerAccount)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsRequired(false);

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

// public string SecurityName { get; set; }
// public string SecurityCode { get; set; }
// public DateTimeOffset? DueDate { get; set; }
// public DateTimeOffset? IssueDate { get; set; }
// public string Asset { get; set; }
// public Counterpart Counterpart { get; set; }

/* Alterações:
 * RF título público
 * - Data Emissão => Data Aquisição (issueDate => purchaseDate)
 * . Precisa mudar serviço (UI ok)
 * - Carteira.Conta CETIP => Conta SELIC
 * . Precisa mudar serviço (UI ok)
 * - Taxa de Emissão => APAGAR (emissionRate)
 * . Precisa mudar serviço (UI ok)
 *
 * RF título privado
 * - Data Emissão => Data Aquisição (issueDate => purchaseDate)
 * . Precisa mudar serviço (UI ok)
 * - Taxa de Emissão => APAGAR (emissionRate)
 * . Precisa mudar serviço (UI ok)
 * - Ativo => Título (UI only)
 * . OK
 *
 * B3
 * - Cotação em R$
 * . UI ok
 * . Verificar se precisa mudar serviço
 * - Carteira.Conta CETIP => Conta B3
 * . Precisa mudar serviço (UI ok)
 * - Contraparte => Corretora Contraparte (UI Only)
 * . OK
 * - (+) Conta broker da Corretora Contraparte
 * . counterpartBrokerAccount (falta serviço)
 */

 /*
  * Alterações pt 2
  * Areas/Ticket/Entities/Portfolio.cs
  * - Account não required
  * - Campos pra Account: {SELIC, CETIP, B3}
  * 
  */

        }
    }
}

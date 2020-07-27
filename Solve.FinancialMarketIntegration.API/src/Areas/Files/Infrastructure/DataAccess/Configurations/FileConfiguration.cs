using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Files.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Files.Infrastructure.DataAccess.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<File> builder)
        {
            builder.ToTable("Files", "Files");

            builder.Property(t => t.Name)
            .IsUnicode(false)
            .HasMaxLength(200);

            builder.Property(t => t.IdFileType)
            .IsUnicode(false);

            builder.Property(t => t.CreationDate)
            .IsUnicode(false);

            builder.Property(t => t.Status)
            .IsUnicode(false);

        }
    }
}

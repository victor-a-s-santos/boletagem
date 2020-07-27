using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Files.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Files.Infrastructure.DataAccess.Configurations
{
    public class FileTypeConfiguration : IEntityTypeConfiguration<FileType>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FileType> builder)
        {
            builder.ToTable("Type", "Files");

            builder.Property(t => t.Name)
            .IsUnicode(false)
            .HasMaxLength(20);
        }
    }
}

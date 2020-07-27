using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Files.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Files.Infrastructure.DataAccess.Configurations
{
    public class FilePESCConfiguration : IEntityTypeConfiguration<FilePESC>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FilePESC> builder)
        {
            builder.ToTable("FilesPESC", "Files");
        }
    }
}

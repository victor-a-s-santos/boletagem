using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Solve.FinancialMarketIntegration.API.Areas.Files.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Files.Infrastructure.DataAccess
{
    public class Seed {
        public static void Apply(ModelBuilder modelBuilder) {
            modelBuilder.Entity<FileType>().HasData(
                new FileType() { Id = 1, Name = "NEGS" },
                new FileType() { Id = 2, Name = "PESQ" }
            );
            
        }

    }
}

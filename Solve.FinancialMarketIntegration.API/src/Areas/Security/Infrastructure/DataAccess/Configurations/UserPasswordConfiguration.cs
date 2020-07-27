using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess.Configurations
{
    public class UserPasswordConfiguration : IEntityTypeConfiguration<UserPassword>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserPassword> builder)
        {
            builder.ToTable("UserPassword", "Security");
        }
    }
}

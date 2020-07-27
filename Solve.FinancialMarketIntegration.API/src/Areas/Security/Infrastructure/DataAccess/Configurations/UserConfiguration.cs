using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "Security");

            builder.HasMany(x => x.PasswordHistory)
                .WithOne();

            builder.Property(t => t.Name)
                   .HasMaxLength(70)
                   .IsRequired();

            builder.Property(t => t.UserName)
                    .HasMaxLength(25);

            builder.Property(t => t.Email)
                   .HasMaxLength(70)
                   .IsRequired();

            builder.Property(t => t.LastAccessToken)
                   .HasMaxLength(250);

            builder.Property(t => t.Salt)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(t => t.PhoneNumber).HasMaxLength(15);
            builder.Property(t => t.Active).IsRequired();
            builder.Property(t => t.LastPasswordChangedDate).IsRequired(false);

            builder.HasIndex(x => x.UserName).IsUnique(true);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class UserAccountManagerConfiguration : IEntityTypeConfiguration<UserAccountManager>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserAccountManager> builder)
        {
            builder.ToTable("UserAccountManager", "Ticket");
            builder.HasKey(uam => new { uam.AccountManagerId, uam.UserIdentifier, uam.IsMaster });
            builder
                .HasOne(uam => uam.AccountManager)
                .WithMany(am => am.UsersAccountManagers)
                .HasForeignKey(uam => uam.AccountManagerId);
        }
    }
}

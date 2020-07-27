using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess.Configurations
{
    public class AccountManagerFundConfiguration : IEntityTypeConfiguration<AccountManagerFund>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AccountManagerFund> builder)
        {
            builder.ToTable("AccountManagerFund", "Ticket");

            builder.HasKey(t => new { t.AccountManagerId, t.FundId });

            builder.HasOne(t => t.Fund)
            .WithMany();

            builder.HasOne(t => t.AccountManager)
            .WithMany();

        }
    }
}

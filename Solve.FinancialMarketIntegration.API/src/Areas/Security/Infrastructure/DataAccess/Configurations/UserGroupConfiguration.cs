using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess.Configurations
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserGroup> builder)
        {
            builder.ToTable("UserGroup", "Security");
            builder.HasKey(gr => new {gr.GroupId, gr.UserId});
            builder
                .HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId);
            builder
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId);
        }
    }
}

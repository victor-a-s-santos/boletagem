using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess.Configurations
{
    public class GroupRoleConfiguration : IEntityTypeConfiguration<GroupRole>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<GroupRole> builder)
        {
            builder.ToTable("GroupRole", "Security");
            builder.HasKey(gr => new {gr.GroupId, gr.RoleId});
            builder
                .HasOne(gr => gr.Group)
                .WithMany(g => g.GroupRoles)
                .HasForeignKey(gr => gr.GroupId);
            builder
                .HasOne(gr => gr.Role)
                .WithMany(r => r.GroupRoles)
                .HasForeignKey(gr => gr.RoleId);
        }
    }
}

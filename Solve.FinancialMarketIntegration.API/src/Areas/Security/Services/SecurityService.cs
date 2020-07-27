using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Security.Models;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Services
{
    public class SecurityService : ISecurityService
    {
        private DataContext Context { get; set; }
        private IUserService UserService { get; set; }
        private IAccountManagerService AccountManagerService { get; set; }

        public SecurityService(DataContext context, IUserService userService, IAccountManagerService accountManagerService)
        {
            this.Context = context;
            this.UserService = userService;
            this.AccountManagerService = accountManagerService;
        }

        public async Task<GroupSchema> EditGroupAsync(int groupId, GroupSchema group, string editUser)
        {
            var groupDb = await Context.Groups
                .SingleOrDefaultAsync(t => t.Id == groupId);

            if (groupDb == null)
            {
                throw new Exception("not-found");
            }

            groupDb.Name = group.Name;
            groupDb.ChangeUser = editUser;
            groupDb.ChangeDate = DateTimeOffset.Now;

            var rolesDb = GetGroupRolesByGroupIdAsync(groupId).Result;

            foreach (var roleDb in rolesDb)
            {
                Context.Remove(roleDb);
            }

            groupDb.GroupRoles = group.Roles.Select(x => new GroupRole()
            {
                GroupId = groupId,
                RoleId = x
            }).ToList();

            await Context.SaveChangesAsync();

            var result = MapResult(groupDb);

            return await Task.FromResult(result);
        }

        public async Task<GroupSchema> GetGroupByIdAsync(int groupId)
        {
            var data = await Context.Groups.SingleOrDefaultAsync(t => t.Id == groupId);

            if (data == null)
            {
                throw new Exception("not-found");
            }

            var result = MapResult(data);

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<GroupSchema>> ListGroupAsync()
        {
            IQueryable<Group> query = Context.Groups;

            var data = await query.ToListAsync();

            if (data.Count == 0)
            {
                return await Task.FromResult(new GroupSchema[] { });
            }

            var result = data
                .Select(group =>
                {
                    var mapped = MapResult(group);

                    return mapped;
                })
                .ToList();

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<GroupSchema>> GetMasterGroupsByUsernameAsync(string username)
        {
            var masterUsername = string.Empty;

            if (AccountManagerService.IsMaster(username))
            {
                masterUsername = username;
            }
            else
            {
                var manager = AccountManagerService.GetAccountManager(username);

                masterUsername = AccountManagerService.GetMasterUsernameFromManager(manager.Id);
            }

            IQueryable<Group> query = Context.Groups;

            var userGroups = Context.UsersGroups.Where(x => x.User.UserName == masterUsername).Select(x => x.GroupId);

            var data = await query.Where(x => userGroups.Contains(x.Id)).ToListAsync();

            if (data.Count == 0)
            {
                return await Task.FromResult(new GroupSchema[] { });
            }

            var result = data
                .Select(group =>
                {
                    var mapped = MapResult(group);

                    return mapped;
                })
                .ToList();

            return await Task.FromResult(result);
        }

        public async Task<GroupSchema> RegisterGroupNewAsync(GroupSchema groupSchema, string creationUser)
        {
            var group = new Group()
            {
                Name = groupSchema.Name,
                CreationDate = DateTimeOffset.Now,
                ChangeDate = DateTimeOffset.Now
            };

            group.GroupRoles = new List<GroupRole>();
            foreach (var role in groupSchema.Roles)
            {
                var groupRole = new GroupRole()
                {
                    GroupId = group.Id,
                    RoleId = role
                };

                group.GroupRoles.Add(groupRole);
            }

            await Context.Groups.AddAsync(group);
            await Context.SaveChangesAsync();

            var result = MapResult(group);

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            IQueryable<Role> query = Context.Roles;

            var data = await query.ToListAsync();

            if (data.Count == 0)
            {
                return await Task.FromResult(new Role[] { });
            }

            return await Task.FromResult(data);
        }

        private async Task<IEnumerable<GroupRole>> GetGroupRolesByGroupIdAsync(int groupId)
        {
            IQueryable<GroupRole> query = Context.GroupsRoles;

            var groupRoles = await query.Where(x => x.GroupId == groupId).ToListAsync();

            return await Task.FromResult(groupRoles);
        }

        private GroupSchema MapResult(Group group)
        {
            var groupRoles = GetGroupRolesByGroupIdAsync(group.Id).Result.Select(x => x.RoleId);

            return new GroupSchema
            {
                Id = group.Id,
                Name = group.Name,
                Roles = groupRoles
            };
        }
    }
}

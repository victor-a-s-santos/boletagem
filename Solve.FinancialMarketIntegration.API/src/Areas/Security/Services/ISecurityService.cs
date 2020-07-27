using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Security.Models;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Services
{
    public interface ISecurityService
    {
        Task<GroupSchema> GetGroupByIdAsync(int groupId);

        Task<IEnumerable<GroupSchema>> ListGroupAsync();

        Task<IEnumerable<GroupSchema>> GetMasterGroupsByUsernameAsync(string username);

        Task<GroupSchema> EditGroupAsync(int userId, GroupSchema group, string editUser);

        Task<GroupSchema> RegisterGroupNewAsync(GroupSchema groupSchema, string creationUser);

        Task<IEnumerable<Role>> GetRolesAsync();
    }
}

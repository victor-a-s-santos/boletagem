using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.Security.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Services
{
    public interface IUserService
    {
        Task<UserSchema> GetByIdAsync(int userId);
        Task<User> GetByUsernameAsync(string username);

        Task<PagedResult<UserSchema>> ListAsync(int? accountManagerId = null, bool isExternalUser = true, int page = 0, int pageSize = 0);

        Task<UserSchema> EditAsync(int userId, UserSchema userSchema, string creationUser);

        Task<UserSchema> RegisterNewAsync(UserSchema userSchema, string creationUser);
        void CreatePasswordHistory(User user, string password);

        Task<MemoryStream> ExportAsync(int? accountManagerId = null, bool isExternalUser = true);
    }
}

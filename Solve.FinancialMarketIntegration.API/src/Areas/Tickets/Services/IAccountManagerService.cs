using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Services
{
    public interface IAccountManagerService
    {
        bool IsAccountManager(string userIdentifier);
        AccountManager GetAccountManager(string userIdentifier);
        IEnumerable<AccountManager> GetAccountManagers();
        List<UserAccountManager> GetUserAccountManagersByUser(string userIdentifier);
        void DeleteUserAccountManagers(IEnumerable<UserAccountManager> userAccountManagers);
        void AddUserAccountManager(UserAccountManager userAccountManager);
        AccountManager GetAccountManagerById(int id);
        List<UserAccountManager> GetUserAccountManagersByAccountManager(int accountManagerId);
        string GetMasterUsernameFromManager(int managerId);
        bool IsMaster(string userIdentifier);
        bool IsInternalUser(string userIdentifier);
        AccountManagerSchema GetAccountManagerSchema(string userIdentifer);
    }
}

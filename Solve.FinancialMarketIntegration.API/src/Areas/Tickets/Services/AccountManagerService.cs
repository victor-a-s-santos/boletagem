using Solve.FinancialMarketIntegration.API.Areas.Security.Models;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Services
{
    public class AccountManagerService : IAccountManagerService
    {
        private TicketDataContext context;
        private IMapper Mapper { get; set; }

        public AccountManagerService(TicketDataContext context,
            IMapper mapper)
        {
            if ((this.context = context) == null) {
                throw new ArgumentNullException("context");
            }
            this.Mapper = mapper;
        }

        public bool IsAccountManager(string userIdentifier)
        {
            return GetAccountManager(userIdentifier) != null;
        }

        public bool IsMaster(string userIdentifier)
        {
            return context.AccountManagers.Any(a => a.UsersAccountManagers.Any(ua => ua.UserIdentifier == userIdentifier && ua.IsMaster));
        }

        public bool IsInternalUser(string userIdentifier)
        {
            return !context.UserAccountManagers.Any(ua => ua.UserIdentifier == userIdentifier);
        }

        public AccountManager GetAccountManager(string userIdentifier)
        {
            var localResult = context.AccountManagers.Local.FirstOrDefault(a => a.UsersAccountManagers.Any(ua => ua.UserIdentifier == userIdentifier));
            if (localResult != null){
                return localResult;
            }
            
            //TODO: guardar no local cache apenas o usuário do userIdentifer ao invés de todos os usuários do account manager
            return context.AccountManagers.Include(a => a.UsersAccountManagers).FirstOrDefault(a => a.UsersAccountManagers.Any(ua => ua.UserIdentifier == userIdentifier));
        }

        public AccountManagerSchema GetAccountManagerSchema(string userIdentifer)
        {
            var accountManager = GetAccountManager(userIdentifer);
            
            var result = Mapper.Map<AccountManagerSchema>(accountManager);

            return result;
        }

        public AccountManager GetAccountManagerById(int id)
        {
            return context.AccountManagers.FirstOrDefault(a => a.Id == id);
        }

        public List<UserAccountManager> GetUserAccountManagersByAccountManager(int accountManagerId)
        {
            return context.UserAccountManagers
                    .Where(a => a.AccountManagerId == accountManagerId)
                    .ToList();
        }

        public IEnumerable<AccountManager> GetAccountManagers()
        {
            return context.AccountManagers
                .OrderBy(x => x.Name)
                .ToList();
        }

        public List<UserAccountManager> GetUserAccountManagersByUser(string userIdentifier)
        {
            return context.UserAccountManagers
                    .Where(a => a.UserIdentifier == userIdentifier)
                    .ToList();
        }

        public void DeleteUserAccountManagers(IEnumerable<UserAccountManager> userAccountManagers)
        {
            context.RemoveRange(userAccountManagers);

            context.SaveChanges();
        }

        public void AddUserAccountManager(UserAccountManager userAccountManager)
        {
            context.Add(userAccountManager);

            context.SaveChanges();
        }

        public string GetMasterUsernameFromManager(int managerId)
        {
            var userAcc = context.UserAccountManagers.FirstOrDefault(x => x.AccountManagerId == managerId && x.IsMaster);

            return userAcc != null ? userAcc.UserIdentifier : string.Empty;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Microsoft.Extensions.Options;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.Security.Models;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using Solve.FinancialMarketIntegration.API.Areas.Files.Services;
using AutoMapper;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Services
{
    public class UserService : IUserService
    {
        private ApplicationSettings ApplicationSettings { get; }
        private DataContext Context { get; set; }
        private IAccountManagerService AccountManagerService { get; set; }
        private IExportService ExportService { get; set; }
        private IEmailService EmailService { get; set; }
        private IHostingEnvironment Environment { get; }
        private IMapper Mapper { get; set; }

        public UserService(
            DataContext context, 
            IAccountManagerService accountManagerService, 
            IOptions<ApplicationSettings> applicationSettings, 
            IEmailService emailService,
            IExportService exportService,
            IHostingEnvironment environment,
            IMapper mapper
        )
        {
            this.Context = context;
            this.AccountManagerService = accountManagerService;
            this.ApplicationSettings = applicationSettings.Value;
            this.EmailService = emailService;
            this.ExportService = exportService;
            this.Environment = environment;
            this.Mapper = mapper;
        }

        public async Task<UserSchema> EditAsync(int userId, UserSchema userSchema, string editUser)
        {
            var userVerification = Context.Users.SingleOrDefault(u => u.UserName == userSchema.UserName);

            if(userVerification != null && userVerification.Id != userId)
            {
                throw new Exception("already-exists");
            }

            var user = Context.Users.SingleOrDefault(t => t.Id == userId);

            if (user == null)
            {
                throw new Exception("not-found");
            }

            var formerUsername = user.UserName;

            Mapper.Map(userSchema, user);

            if(!userSchema.LockoutEndDateUtc.HasValue && !userSchema.LockoutEnabled)
            {
                user.LockoutEnabled = false;
                user.ResetAccessFailedCount();
            } 
            else
            {
                user.LockoutEnabled = userSchema.LockoutEnabled || userSchema.LockoutEndDateUtc.Value >= DateTimeOffset.UtcNow;
            }

            var groupsDb = GetUserGroupsByUserIdAsync(userId).Result;

            foreach(var groupDb in groupsDb)
            {
                Context.Remove(groupDb);
            }

            if(userSchema.Groups != null)
            { 
                user.UserGroups = userSchema.Groups.Select(x => new UserGroup()
                {
                    GroupId = x,
                    UserId = userId
                }).ToList();
            }

            if (userSchema.IsExternalUser && userSchema.AccountManagerId.HasValue)
            {
                TratarUserAccountManagerAsync(userSchema.IsMaster, formerUsername, userSchema.UserName, editUser, userSchema.AccountManagerId.Value);
            }

            Context.SaveChanges();

            return await Task.FromResult(MapResult(user));
        }

        public async Task<UserSchema> GetByIdAsync(int userId)
        {
            var data = Context.Users.SingleOrDefault(t => t.Id == userId);

            if (data == null)
            {
                throw new Exception("not-found");
            }

            return await Task.FromResult(MapResult(data));
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await Task.FromResult(Context.Users.SingleOrDefault(t => t.UserName == username));
        }

        public async Task<PagedResult<UserSchema>> ListAsync(int? accountManagerId = null, bool isExternalUser = true, int page = 0, int pageSize = 0)
        {
            IQueryable<User> query = Context.Users;

            if (accountManagerId.HasValue)
            {
                var accManager = AccountManagerService.GetAccountManagerById(accountManagerId.Value);

                if (accManager != null)
                {
                    var userNames = AccountManagerService.GetUserAccountManagersByAccountManager(accManager.Id)
                        .Select(x => x.UserIdentifier);

                    if (userNames != null)
                        query = query.Where(x => userNames.Contains(x.UserName));
                }
            }
            else
            {
                if (!isExternalUser)
                {
                    query = query.Where(x => AccountManagerService.IsInternalUser(x.UserName));
                }
                else
                {
                    query = query.Where(x => !AccountManagerService.IsInternalUser(x.UserName));
                }
            }

            var pagedResult = query.GetPaged(page, pageSize);
            var listUsers = pagedResult.Results
                .Select(user =>
                {
                    var mapped = MapResult(user);

                    return mapped;
                })
                .ToList();

            PagedResult<UserSchema> result = new PagedResult<UserSchema>()
            {
                Results = listUsers,
                PageCount = pagedResult != null ? pagedResult.PageCount : 0,
                RowCount = pagedResult != null ? pagedResult.RowCount : 0,
                PageSize = pagedResult != null ? pagedResult.PageSize : 0,
                CurrentPage = pagedResult != null ? pagedResult.CurrentPage : 0,
                FirstRowOnPage = pagedResult != null ? pagedResult.FirstRowOnPage : 0,
                LastRowOnPage = pagedResult != null ? pagedResult.LastRowOnPage : 0
            };

            return await Task.FromResult(result);
        }

        public async Task<UserSchema> RegisterNewAsync(UserSchema userSchema, string creationUser)
        {
            var userVerification = Context.Users.SingleOrDefault(
                u => u.UserName == userSchema.UserName
            );

            if (userVerification != null)
            {
                throw new Exception("already-exists");
            }

            byte[] salt = new byte[128 / 8];
            byte[] senhaBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
                rng.GetBytes(senhaBytes);
            }

            var senha = Convert.ToBase64String(senhaBytes);

            string passwordHash = Services.AuthService.GetHashed(senha, Convert.ToBase64String(salt));

            var user = Mapper.Map<User>(userSchema);

            user.PasswordExpirationDate = DateTimeOffset.Now;
            user.CreationDate = DateTimeOffset.Now;
            user.ChangeDate = DateTimeOffset.Now;
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;
            user.Salt = Convert.ToBase64String(salt);
            user.PasswordHash = passwordHash;

            if (!userSchema.LockoutEndDateUtc.HasValue && !userSchema.LockoutEnabled)
            {
                user.LockoutEnabled = false;
                user.ResetAccessFailedCount();
            }
            else
            {
                user.LockoutEnabled = userSchema.LockoutEnabled || userSchema.LockoutEndDateUtc.Value >= DateTimeOffset.Now;
            }

            CreatePasswordHistory(user, passwordHash);

            user.UserGroups = new List<UserGroup>();

            if(userSchema.Groups != null)
            {
                foreach (var group in userSchema.Groups)
                {
                    var userGroup = new UserGroup()
                    {
                        GroupId = group,
                        UserId = user.Id
                    };

                    user.UserGroups.Add(userGroup);
                }
            }

            if (userSchema.IsExternalUser && userSchema.AccountManagerId.HasValue)
            {
                TratarUserAccountManagerAsync(userSchema.IsMaster, userSchema.UserName, userSchema.UserName, creationUser, userSchema.AccountManagerId.Value);
            } 

            Context.Users.Add(user);

            Context.SaveChanges();

            var dictValoresEmail = new Dictionary<string, string>();

            dictValoresEmail.Add("[[NOME]]", userSchema.Name);
            dictValoresEmail.Add("[[USERNAME]]", userSchema.UserName);
            dictValoresEmail.Add("[[SENHA]]", senha);
            dictValoresEmail.Add("[[URL]]", ApplicationSettings.Url);

            await this.EmailService.SendEmailAsync(userSchema.Email, "Criação de Usuário", null, Entities.Enums.EmailTemplateEnum.PasswordCreation, dictValoresEmail);

            return await Task.FromResult(MapResult(user));
        }

        public void CreatePasswordHistory(User user, string password)
        {
            if(user.PasswordHistory == null)
            {
                user.PasswordHistory = new List<UserPassword>();
            }

            user.PasswordHistory.Add(new UserPassword()
            {
                PasswordHash = password,
                CreationDate = DateTimeOffset.Now,
                UserId = user.Id
            });
        }

        public async Task<MemoryStream> ExportAsync(int? accountManagerId = null, bool isExternalUser = true)
        {
            var users = await ListAsync(accountManagerId, isExternalUser);

            var mapper = new ColumnMap<UserSchema>[] {
                new ColumnMap<UserSchema>("ID", (u) => u.Id),
                new ColumnMap<UserSchema>("NOME", (u) => u.Name),
                new ColumnMap<UserSchema>("E-MAIL", (u) => u.Email),
                new ColumnMap<UserSchema>("USERNAME", (u) => u.UserName),
                new ColumnMap<UserSchema>("CPF", (u) => u.UserDocument),
                new ColumnMap<UserSchema>("GRUPOS", (u) => string.Join(",", Context.Groups.Where(g => u.Groups.Contains(g.Id)).Select(g => g.Name).ToArray())),
                new ColumnMap<UserSchema>("ATIVO", (u) => u.Active ? "Sim" : "Não"),
                new ColumnMap<UserSchema>("BLOQUEADO ATÉ", (u) => u.LockoutEndDateUtc.HasValue ? u.LockoutEndDateUtc.Value.ToString() : string.Empty)
            };

            ExcelPackage excelPackage = null;

            using (excelPackage = new ExcelPackage())
            {
                ExportService.GenerateWorksheet<UserSchema>(excelPackage, "Usuários", mapper, users.Results);

                return new MemoryStream(excelPackage.GetAsByteArray());
            }
        }

        private UserSchema MapResult(User user)
        {
            var userGroups = GetUserGroupsByUserIdAsync(user.Id).Result.Select(x => x.GroupId);
            var accManager = AccountManagerService.GetAccountManager(user.UserName);

            if(accManager == null)
            {
                accManager = new AccountManager();
            }

            var userSchema = Mapper.Map<UserSchema>(user);
            userSchema.AccountManagerId = accManager.Id;
            userSchema.IsExternalUser = !AccountManagerService.IsInternalUser(user.UserName);
            userSchema.IsMaster = AccountManagerService.IsMaster(user.UserName);
            userSchema.Groups = userGroups;

            return userSchema;
        }

        private async Task<IEnumerable<UserGroup>> GetUserGroupsByUserIdAsync(int userId)
        {
            IQueryable<UserGroup> query = Context.UsersGroups;

            var userGroups = await query.Where(x => x.UserId == userId).ToListAsync();

            return await Task.FromResult(userGroups);
        }

        private void TratarUserAccountManagerAsync(bool isMaster, string formerUsername, string username, string userAction, int accountManagerId)
        {
            // Deletar os UserAccountManager existentes
            var userAccManagers = AccountManagerService.GetUserAccountManagersByUser(formerUsername);

            if(userAccManagers != null)
            {
                this.AccountManagerService.DeleteUserAccountManagers(userAccManagers);
            }

            var userAccountManager = new UserAccountManager()
            {
                IsMaster = isMaster,
                UserIdentifier = username,
                CreationDate = DateTimeOffset.Now,
                CreationUser = userAction,
                AccountManagerId = accountManagerId
            };

            this.AccountManagerService.AddUserAccountManager(userAccountManager);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;
using Solve.FinancialMarketIntegration.API.Areas.Security.Models;
using Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess;
using Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.Helpers;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Services
{
    public class AuthService : IAuthService
    {
        private ApplicationSettings ApplicationSettings { get; }
        private DataContext Context { get; set; }
        private IHostingEnvironment Environment { get; }

        private readonly JWTTokenSettings jwtTokenSettings;
        private readonly PasswordSettings passwordSettings;
        private readonly UserSettings userSettings;

        private IUserService UserService { get; set; }
        private readonly IEmailService EmailService;
        private readonly IConfiguration Configuration;

        private IAccountManagerService AcoountManager { get; set; }

        public AuthService(
            IOptions<JWTTokenSettings> jwtTokenSettings, 
            IOptions<PasswordSettings> passwordSettings, 
            IOptions<UserSettings> userSettings, 
            DataContext context, 
            IUserService userService, 
            IEmailService emailService,
            IOptions<ApplicationSettings> applicationSettings,
            IHostingEnvironment environment,
            IConfiguration configuration,
            IAccountManagerService accountManager)
        {
            this.jwtTokenSettings = jwtTokenSettings.Value;
            this.passwordSettings = passwordSettings.Value;
            this.userSettings = userSettings.Value;
            this.Context = context;
            this.UserService = userService;
            this.EmailService = emailService;
            this.ApplicationSettings = applicationSettings.Value;
            this.Environment = environment;
            this.Configuration = configuration;
            this.AcoountManager = accountManager;
        }

        public UserAuthentication PasswordReset(string username, string newPassword, string oldPassword = null, string passwordResetHash = null)
        {
            if(!string.IsNullOrEmpty(oldPassword)) 
            {
                var userAuth = Authenticate(username, oldPassword, true);

                if (userAuth == null)
                {
                    throw new Exception("wrong-password");
                }
            }

            // Senha antiga correta ou hash válido, fluxo segue
            var user = Context.Users.SingleOrDefault(u => u.UserName == username && u.Active && u.LockoutEnabled == false);

            if(user == null || passwordResetHash != user.PasswordResetHash) {
                throw new Exception("user-not-valid");
            }

            user.PasswordHash = GetHashed(newPassword, user.Salt);

            if (ExistsInPasswordHistory(user.Id, user.PasswordHash))
            {
                throw new Exception("password-history");
            }
            
            if (!this.IsValidPassword(newPassword))
            {
                throw new Exception("invalid-password");
            }

            user.PasswordExpirationDate = DateTimeOffset.Now.AddDays(this.passwordSettings.ExpirationDays);
            user.PasswordResetHash = null;

            this.UserService.CreatePasswordHistory(user, user.PasswordHash);

            Context.SaveChanges();

            return GenerateAuthUser(user);
        }

        public UserAuthentication Authenticate(string username, string password, bool ignoreExpiredPassword = false)
        {
            var user = Context.Users.SingleOrDefault(u => u.UserName == username && u.Active && u.LockoutEnabled == false);

            if (user == null)
            {
                throw new Exception("user-not-valid");
            }

            string hashedPassword = AuthService.GetHashed(password, user.Salt);

            if (user.PasswordHash != hashedPassword)
            {
                user.IncriseAccessFailedCount(this.passwordSettings.AttemptsLimit);
                Context.SaveChanges();

                throw new Exception("wrong-password");
            }

            if (user.PasswordExpirationDate <= DateTimeOffset.Now && !ignoreExpiredPassword)
            {
                throw new Exception("password-expired");
            }

            if (user.LastAccessDate.HasValue)
            {
                if (System.DateTimeOffset.Now.Subtract(user.LastAccessDate.Value).TotalDays > this.userSettings.MaximumInactiveDays)
                {
                    user.LockoutEnabled = true;
                    this.Context.SaveChanges();
                    throw new Exception("user-not-valid");
                }
            }
            

            return GenerateAuthUser(user);
        }

        public IEnumerable<UserData> GetUserNames(IEnumerable<string> usernames)
        {
            var data = Context.Users
                .Where(u => usernames.Contains(u.UserName))
                .Select(u => new UserData() {
                    User = u.UserName,
                    Name = u.Name
                })
                .ToList();

            return data;
        }

        public bool HasRoles(string username, IEnumerable<int> roles)
        {
            var userRoles = GetRoleQuery(username);
            return userRoles.Any(r => roles.Contains(r.Id));
        }

        public IEnumerable<Role> GetRoles(ClaimsPrincipal user)
        {
            return this.GetRoles(user.GetUsername());
        }

        public IEnumerable<Role> GetRoles(string username)
        {
            var roles = this.Context.Users.Where(u => u.UserName.Equals(username))
                              .SelectMany(u => u.UserGroups)
                              .Select(ug => ug.Group)
                              .SelectMany(g => g.GroupRoles)
                              .Select(gr => gr.Role)
                              .Distinct()
                              .ToArray();

            return roles;
        }

        public async Task PasswordForgot(string username)
        {
            var user = Context.Users.SingleOrDefault(u => u.UserName == username && u.Active && u.LockoutEnabled == false);

            if (user == null)
            {
                throw new Exception("user-not-valid");
            }

            byte[] random = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
            }

            var hash = ByteArrayToString(random);

            user.PasswordResetHash = hash;

            Context.SaveChanges();

            var dictValoresEmail = new Dictionary<string, string>();
            var url = string.Concat(ApplicationSettings.Url, "/password-reset/", user.UserName, "/", hash);

            dictValoresEmail.Add("[[NOME]]", user.Name);
            dictValoresEmail.Add("[[URL]]", url);

            await this.EmailService.SendEmailAsync(user.Email, "Reset de Senha", null, Entities.Enums.EmailTemplateEnum.PasswordForgot, dictValoresEmail);
        }

        private UserAuthentication GenerateAuthUser(User authenticatedUser)
        {
            var user = new UserAuthentication()
            {
                Name = authenticatedUser.Name,
                Password = null,
                Username = authenticatedUser.UserName,
                isAccountManager = this.AcoountManager.IsAccountManager(authenticatedUser.UserName)
            };

            this.GenerateToken(user);
            authenticatedUser.RegisterAccess(user.Token);
            
            Context.SaveChanges();

            return user;
        }

        private void GenerateToken(UserAuthentication user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.jwtTokenSettings.Key);
            var expirationDate = DateTime.UtcNow.AddDays(this.jwtTokenSettings.ExpirationDays);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.TokenExpiration = expirationDate;
        }

        private IQueryable<Role> GetRoleQuery(string username)
        {
            return this.Context.Users.Where(u => u.UserName.Equals(username))
                              .SelectMany(u => u.UserGroups)
                              .Select(ug => ug.Group)
                              .SelectMany(g => g.GroupRoles)
                              .Select(gr => gr.Role)
                              .Distinct();
        }

        public static string GetHashed(string input, string salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: input,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        private bool ExistsInPasswordHistory(int userId, string password)
        {
            List<UserPassword> mostRecentPasswords = this.Context.UserPasswords
                                                                    .Where(up => up.UserId == userId)
                                                                    .OrderByDescending(up => up.CreationDate)
                                                                    .Take(this.passwordSettings.HistoryAge)
                                                                    .ToList();
            return mostRecentPasswords.Any(up => up.PasswordHash == password);
                                
        }

        private bool IsValidPassword(string password)
        {
            // min length
            if (password.Length < 8)
            {
                return false;
            }
            int amounfOfRulesVerified = 0;
            amounfOfRulesVerified += AuthService.FindRegexInPassword("[A-Z]", password) ? 1: 0;
            amounfOfRulesVerified += AuthService.FindRegexInPassword("[a-z]", password) ? 1: 0;
            amounfOfRulesVerified += AuthService.FindRegexInPassword("[0-9]", password) ? 1: 0;
            amounfOfRulesVerified += AuthService.FindRegexInPassword("[@#&]", password) ? 1: 0;
            if (amounfOfRulesVerified < 3)
            {
                return false;
            }
            return true;
        }

        private static bool FindRegexInPassword(string regex, string password)
        {
            Regex rx = new Regex(regex);
            return rx.Matches(password).Count > 0;
        }

        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
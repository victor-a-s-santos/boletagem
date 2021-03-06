using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Solve.FinancialMarketIntegration.API.Areas.Security.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Security.Infrastructure.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserGroup> UsersGroups { get; set; }
        public DbSet<GroupRole> GroupsRoles { get; set; }
        public DbSet<UserPassword> UserPasswords { get; set; }

        private IHttpContextAccessor httpContextAcessor;

        public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.httpContextAcessor = httpContextAccessor;
        }

        // DEBUG
        public static readonly LoggerFactory MyLoggerFactory
            = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                                 .Where(t => t.GetInterfaces().Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) && t.Namespace.Contains("Security")))
                                 .ToArray();


            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            Seed.Apply(modelBuilder);
        }

        // DEBUG
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            CheckChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Método responsável em detectar as mudanças de entidades e direcionar para as classes
        /// que observam mudanças nessas classes
        /// </summary>
        private void CheckChanges()
        {
            var trackersChange = Assembly.GetExecutingAssembly().GetTypes()
                                                            .Where(t => !t.IsAbstract &&
                                                                        typeof(IAuditable).IsAssignableFrom(t))
                                                            .ToArray();

            var trackedEntities = ChangeTracker.Entries()
                                               .Where(e => typeof(IAuditable).IsAssignableFrom(e.Entity.GetType()))
                                               .ToArray();

            foreach (var entry in trackedEntities)
            {
                var entity = entry.Entity as IAuditable;

                if (entry.State == EntityState.Added)
                {
                    if (entity.CreationDate == default(DateTimeOffset))
                        entity.CreationDate = DateTimeOffset.Now;

                    if (String.IsNullOrEmpty(entity.CreationUser))
                        entity.CreationUser = CurrentUser;
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entity.ChangeDate == default(DateTimeOffset?))
                        entity.ChangeDate = DateTimeOffset.Now;

                    if (String.IsNullOrEmpty(entity.ChangeUser))
                        entity.ChangeUser = CurrentUser;
                }

            }
        }

        private string currentUser;
        public string CurrentUser
        {
            get
            {
                if (String.IsNullOrEmpty(currentUser))
                {
                    currentUser = httpContextAcessor.HttpContext.User.Identity.Name;
                }

                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }
    }
}

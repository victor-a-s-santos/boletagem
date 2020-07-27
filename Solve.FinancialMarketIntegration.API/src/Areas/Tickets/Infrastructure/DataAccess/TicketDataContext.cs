using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Infrastructure.DataAccess
{
    public class TicketDataContext : DbContext
    {
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<SettlementType> SettlementTypes { get; set; }
        public DbSet<MarketType> MarketTypes { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketPrivateFixedIncome> TicketPrivateFixedIncomes { get; set; }
        public DbSet<TicketFundQuotas> TicketFundQuotas { get; set; }
        public DbSet<Fund> Funds { get; set; }
        public DbSet<TicketPublicFixedIncome> TicketPublicFixedIncomes { get; set; }
        public DbSet<TicketContracted> TicketContracted { get; set; }
        public DbSet<TicketFutures> TicketFutures { get; set; }
        public DbSet<TicketSwapCetip> TicketSwapCetip { get; set; }
        public DbSet<TicketMargin> TicketMargin { get; set; }
        public DbSet<TicketCurrencyTerm> TicketCurrencyTerm { get; set; }
        public DbSet<AccountManager> AccountManagers { get; set; }
        public DbSet<TicketVariableIncome> TicketVariableIncome { get; set; }
        public DbSet<TicketVariableIncomeItem> TicketVariableIncomeItems { get; set; }
        public DbSet<UserAccountManager> UserAccountManagers { get; set; }

        public DbSet<TicketSignoff> TicketSignoff { get; set; }

        public DbSet<TicketSignoffLog> TicketSignoffLog { get; set; }

        public DbSet<TicketSignoffRequest> TicketSignoffRequest { get; set; }
        public DbSet<EventType> EventTypes { get; set; }

        public DbSet<Bank> Bank { get; set; }


        public TicketDataContext(DbContextOptions<TicketDataContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.httpContextAcessor = httpContextAccessor;
        }

        private IHttpContextAccessor httpContextAcessor;

        // DEBUG
        public static readonly LoggerFactory MyLoggerFactory
            = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                                                                  .Where(t => t.GetInterfaces().Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) && t.Namespace.Contains("Tickets")))
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
            optionsBuilder.EnableSensitiveDataLogging();
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
                    entity.ChangeDate = DateTimeOffset.Now;
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

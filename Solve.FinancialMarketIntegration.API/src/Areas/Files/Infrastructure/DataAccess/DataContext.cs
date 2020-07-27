using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Solve.FinancialMarketIntegration.API.Areas.Files.Entities;
using TicketEntities = Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

namespace Solve.FinancialMarketIntegration.API.Areas.Files.Infrastructure.DataAccess
{
    public class FileDataContext : DbContext
    {
        public DbContextOptions<FileDataContext> options { get; set; }
        public DbSet<FileType> FileTypes { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<FilePESC> FilePESC { get; set; }

        public FileDataContext(DbContextOptions<FileDataContext> options) : base(options)
        {

        }



        // DEBUG
        public static readonly LoggerFactory MyLoggerFactory
            = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });
        private Func<object, object> p;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                                                                  .Where(t => t.GetInterfaces().Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) && t.Namespace.Contains("Files")))
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

    }
}

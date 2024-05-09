using NCA.Common.Scheduler.Jobs;
using Microsoft.EntityFrameworkCore;

namespace NCA.COL.Infrastructure.Repositories
{
    public class SchedulerDbContext : DbContext
    {
        private readonly string? _connectionString;

        public DbSet<ApiRestData>? ApiRestDatas { get; set; }

        public SchedulerDbContext(string connectionString)
            : base()
        {
            _connectionString = connectionString;
        }

        public SchedulerDbContext(IConfiguration configuration)
            : base()
        {
            _connectionString = configuration.GetValue<string>("ConnectionString");
        }

        // csharpier-ignore
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString))
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()))
                ;
        }
    }
}

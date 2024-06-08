using Microsoft.EntityFrameworkCore;
using NCA.Common.Application.Exceptions;
using NCA.Common.Scheduler.Jobs;

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
            _connectionString = configuration["ConnectionString"];

            if (string.IsNullOrEmpty(_connectionString))
                throw new ConfigurationException("ConnectionString parameter is required.");
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

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NCA.Production.Application.Services;
using NCA.Production.Domain.Contracts.Repositories;
using NCA.Production.Infrastructure.Repositories;
using NCA.Production.Infrastructure.Services;

namespace NCA.Production.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AdventureWorksDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}

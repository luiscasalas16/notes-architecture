using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NCA.Tracks.Infrastructure.Repositories;
using NCA.Tracks.Infrastructure.Services;

namespace NCA.Tracks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChinookContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IAlbumRepository, AlbumRepository>();

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}

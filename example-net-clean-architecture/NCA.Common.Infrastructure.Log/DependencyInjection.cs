using Microsoft.Extensions.DependencyInjection;
using NCA.Common.Application.Infrastructure.Log;

namespace NCA.Common.Infrastructure.Log
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureCommonLoggers(this IServiceCollection services)
        {
            services.AddScoped<ILogger, Logger>();

            return services;
        }
    }
}

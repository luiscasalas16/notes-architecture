using NCA.Common.Api.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace NCA.Common.Api.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonSwagger(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            //swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
                //fix the error System.InvalidOperationException: Can't use schemaId "$MyType" for type "$OneNamespace.MyType". The same schemaId is already used for type "$OtherNamespace.MyType".
                options.CustomSchemaIds(s => s.FullName!.Replace("+", "."));
            });

            return services;
        }

        public static IServiceCollection AddCommonExceptionHandler(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.Configure<RouteHandlerOptions>(o =>
            {
                o.ThrowOnBadRequest = true;
            });
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            return services;
        }

        public static IServiceCollection AddCommonHealthCheck(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddHealthChecks();

            return services;
        }

        public static IApplicationBuilder UseCommonSwagger(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);

            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }

        public static IApplicationBuilder UseCommonExceptionHandler(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);

            app.UseExceptionHandler();

            return app;
        }

        public static IApplicationBuilder UseCommonHealthCheck(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);

            // https://learn.microsoft.com/en-us/azure/application-gateway/application-gateway-probe-overview#default-health-probe
            app.UseHealthChecks("/");

            return app;
        }
    }
}

using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NCA.Common.Api.Exceptions;
using NCA.Common.ApiRest.Helpers;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NCA.Common.Api.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonSwagger(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            //swagger
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, CommonSwaggeOptions>();
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
                //fix the error System.InvalidOperationException: Can't use schemaId "$MyType" for type "$OneNamespace.MyType". The same schemaId is already used for type "$OtherNamespace.MyType".
                options.CustomSchemaIds(s => s.FullName!.Replace("+", "."));
            });

            return services;
        }

        public static IApplicationBuilder UseCommonSwagger(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var descriptions = ((WebApplication)app).DescribeApiVersions();

                // build a swagger endpoint for each discovered API version
                foreach (var groupName in descriptions.Select(t => t.GroupName))
                {
                    var url = $"/swagger/{groupName}/swagger.json";
                    var name = groupName.ToUpperInvariant();
                    options.SwaggerEndpoint(url, name);
                }
            });

            return app;
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

        public static IApplicationBuilder UseCommonExceptionHandler(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);

            app.UseExceptionHandler();

            return app;
        }

        public static IServiceCollection AddCommonHealthCheck(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddHealthChecks();

            return services;
        }

        public static IApplicationBuilder UseCommonHealthCheck(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);

            // https://learn.microsoft.com/en-us/azure/application-gateway/application-gateway-probe-overview#default-health-probe
            app.UseHealthChecks("/");

            return app;
        }

        public static IServiceCollection AddCommonVersioning(this IServiceCollection services, int? defaultVersion = null)
        {
            ArgumentNullException.ThrowIfNull(services);

            // Add services to the container.
            services.AddEndpointsApiExplorer();
            services
                .AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(defaultVersion ?? 1);
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
                })
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            return services;
        }

        public static IApplicationBuilder UseCommonVersioning(this IApplicationBuilder app, int[]? versions = null)
        {
            ArgumentNullException.ThrowIfNull(app);

            if (versions != null)
            {
                var apiVersionBuilder = ((WebApplication)app).NewApiVersionSet();

                foreach (var version in versions)
                    apiVersionBuilder.HasApiVersion(new ApiVersion(version));

                apiVersionBuilder.ReportApiVersions().Build();
            }
            else
            {
                ((WebApplication)app).NewApiVersionSet().HasApiVersion(new ApiVersion(1)).ReportApiVersions().Build();
            }

            return app;
        }
    }
}

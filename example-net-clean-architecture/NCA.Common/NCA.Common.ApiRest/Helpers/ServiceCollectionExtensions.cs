using System;
using System.Reflection;
using System.Xml.XPath;
using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NCA.Common.Api.Exceptions;
using NCA.Common.ApiRest.Helpers;

namespace NCA.Common.Api.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder AddCommonVersions(this WebApplicationBuilder webApplicationBuilder, int? defaultVersion = null, int[]? versions = null)
        {
            ArgumentNullException.ThrowIfNull(webApplicationBuilder);

            webApplicationBuilder.Services.AddEndpointsApiExplorer();

            webApplicationBuilder
                .Services.AddApiVersioning(options =>
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

            webApplicationBuilder.Services.Configure<ServiceArquitectureOptions.Versions>(options =>
            {
                options.ApiVersions = versions != null && versions.Length != 0 ? versions : [1];
            });

            return webApplicationBuilder;
        }

        public static WebApplication UseCommonVersions(this WebApplication webApplication)
        {
            ArgumentNullException.ThrowIfNull(webApplication);

            var versions = webApplication.Services.GetRequiredService<IOptionsMonitor<ServiceArquitectureOptions.Versions>>().CurrentValue.ApiVersions;

            if (versions != null && versions.Length != 0)
            {
                var apiVersionBuilder = webApplication.NewApiVersionSet();

                foreach (var version in versions)
                    apiVersionBuilder.HasApiVersion(new ApiVersion(version));

                apiVersionBuilder.ReportApiVersions().Build();
            }
            else
            {
                webApplication.NewApiVersionSet().HasApiVersion(new ApiVersion(1)).ReportApiVersions().Build();
            }

            return webApplication;
        }

        public static WebApplicationBuilder AddCommonExceptionHandler(this WebApplicationBuilder webApplicationBuilder)
        {
            ArgumentNullException.ThrowIfNull(webApplicationBuilder);

            webApplicationBuilder.Services.Configure<RouteHandlerOptions>(o =>
            {
                o.ThrowOnBadRequest = true;
            });
            webApplicationBuilder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            webApplicationBuilder.Services.AddProblemDetails();

            return webApplicationBuilder;
        }

        public static WebApplication UseCommonExceptionHandler(this WebApplication webApplication)
        {
            ArgumentNullException.ThrowIfNull(webApplication);

            webApplication.UseExceptionHandler();

            return webApplication;
        }

        public static WebApplicationBuilder AddCommonHealthCheck(this WebApplicationBuilder webApplicationBuilder)
        {
            ArgumentNullException.ThrowIfNull(webApplicationBuilder);

            webApplicationBuilder.Services.AddHealthChecks();

            return webApplicationBuilder;
        }

        public static WebApplication UseCommonHealthCheck(this WebApplication webApplication)
        {
            ArgumentNullException.ThrowIfNull(webApplication);

            // https://learn.microsoft.com/en-us/azure/application-gateway/application-gateway-probe-overview#default-health-probe
            webApplication.UseHealthChecks("/");

            return webApplication;
        }

        public static WebApplicationBuilder AddCommonSwagger(this WebApplicationBuilder webApplicationBuilder)
        {
            ArgumentNullException.ThrowIfNull(webApplicationBuilder);

            using (ServiceProvider serviceProvider = webApplicationBuilder.Services.BuildServiceProvider())
            {
                var versions = serviceProvider.GetRequiredService<IOptionsMonitor<ServiceArquitectureOptions.Versions>>().CurrentValue.ApiVersions ?? [1];

                //swagger
                webApplicationBuilder.Services.AddEndpointsApiExplorer();
                webApplicationBuilder.Services.AddSwaggerGen(options =>
                {
                    //fix the error System.InvalidOperationException: Can't use schemaId "$MyType" for type "$OneNamespace.MyType". The same schemaId is already used for type "$OtherNamespace.MyType".
                    options.CustomSchemaIds(x => x.FullName);
                    options.CustomSchemaIds(s => s.FullName!.Replace("+", "."));

                    //register api versions
                    foreach (var version in versions)
                    {
                        options.SwaggerDoc(
                            $"v{version}",
                            new OpenApiInfo() { Title = $"{Assembly.GetEntryAssembly()?.ManifestModule.Name.Replace(".dll", string.Empty)} v{version}", Version = $"v{version}" }
                        );
                    }
                });
            }

            return webApplicationBuilder;
        }

        public static WebApplication UseCommonSwagger(this WebApplication webApplication)
        {
            ArgumentNullException.ThrowIfNull(webApplication);

            string? apiGatewayPath = webApplication.Configuration["API_GATEWAY_PATH"];

            if (webApplication.Environment.IsDevelopment())
            {
                var versions = webApplication.Services.GetRequiredService<IOptionsMonitor<ServiceArquitectureOptions.Versions>>().CurrentValue.ApiVersions ?? [1];

                if (string.IsNullOrWhiteSpace(apiGatewayPath))
                {
                    webApplication.UseSwagger();
                }
                else
                {
                    webApplication.UseSwagger(c =>
                    {
                        c.PreSerializeFilters.Add(
                            (options, httpRequest) =>
                            {
                                options.Servers = [new OpenApiServer { Url = $"/{apiGatewayPath}" }];
                            }
                        );
                    });
                }

                webApplication.UseSwaggerUI(options =>
                {
                    foreach (var version in versions)
                        options.SwaggerEndpoint($"v{version}/swagger.json", $"v{version}");
                });
            }

            return webApplication;
        }
    }
}

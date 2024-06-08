using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NCA.Common.ApiRest.Helpers;
using NCA.Common.Application.Exceptions;

namespace NCA.Common.Api.Endpoints
{
    public abstract class EndpointsMapper
    {
        public WebApplication WebApplication { get; private set; }
        public string Name { get; private set; }
        public int[] Versions { get; private set; }

        protected EndpointsMapper(WebApplication webApplication)
        {
            WebApplication = webApplication;
            Name = GetType().Name;
            Versions = webApplication.Services.GetRequiredService<IOptionsMonitor<ServiceArquitectureOptions.Versions>>().CurrentValue.ApiVersions ?? [1];
        }

        public abstract void Map();

        public EndpointsGroup Group(int[]? versions = null)
        {
            var routeGroupBuilder = WebApplication.MapGroup("/api/v{version:apiVersion}/" + Name).WithTags(Name).WithOpenApi().WithApiVersionSet(BuildVersions($"/{Name}", versions));

            return new EndpointsGroup(this, routeGroupBuilder);
        }

        private ApiVersionSet BuildVersions(string name, int[]? versions = null)
        {
            var apiVersionBuilder = WebApplication.NewApiVersionSet();

            if (versions != null && versions.Length != 0)
            {
                foreach (var version in versions)
                {
                    if (!Versions.Contains(version))
                        throw new ConfigurationException($"The version {version} registered to {name} does not exist.");

                    apiVersionBuilder.HasApiVersion(version);
                }
            }
            else
            {
                foreach (var avaliableVersion in Versions)
                    apiVersionBuilder.HasApiVersion(avaliableVersion);
            }

            return apiVersionBuilder.Build();
        }
    }
}

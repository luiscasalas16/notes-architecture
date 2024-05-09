using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NCA.Common.Api.Endpoints;
using NCA.Common.ApiRest.Helpers;
using NCA.Common.Application.Exceptions;

namespace NCA.Common.Api.Endpoints
{
    public abstract class EndpointsMapper
    {
        public readonly WebApplication _webApplication;

        private readonly string _name;
        public readonly int[] _versions;

        public string Name => _name;

        protected EndpointsMapper(WebApplication webApplication)
        {
            _webApplication = webApplication;
            _name = GetType().Name;
            _versions = webApplication.Services.GetRequiredService<IOptionsMonitor<ServiceArquitectureOptions.Versions>>().CurrentValue.ApiVersions;
        }

        public abstract void Map();

        public EndpointsGroup Group(int[]? versions = null)
        {
            var routeGroupBuilder = _webApplication.MapGroup("/api/v{version:apiVersion}/" + _name).WithTags(_name).WithOpenApi().WithApiVersionSet(BuildApiVersionSet($"/{_name}", versions));

            return new EndpointsGroup(this, routeGroupBuilder);
        }

        public ApiVersionSet BuildApiVersionSet(string name, int[]? versions = null)
        {
            var apiVersionBuilder = _webApplication.NewApiVersionSet();

            if (versions != null && versions.Length != 0)
            {
                foreach (var version in versions)
                {
                    if (!_versions.Contains(version))
                        throw new ConfigurationException($"The version {version} registered to {name} does not exist.");

                    apiVersionBuilder.HasApiVersion(version);
                }
            }
            else
            {
                foreach (var avaliableVersion in _versions)
                    apiVersionBuilder.HasApiVersion(avaliableVersion);
            }

            return apiVersionBuilder.Build();
        }
    }
}

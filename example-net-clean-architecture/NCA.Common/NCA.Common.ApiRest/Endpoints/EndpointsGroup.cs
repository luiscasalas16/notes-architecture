using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using NCA.Common.Application.Exceptions;

namespace NCA.Common.Api.Endpoints
{
    public class EndpointsGroup
    {
        private readonly EndpointsMapper _endpointMapper;
        private readonly RouteGroupBuilder _routeGroupBuilder;

        public EndpointsGroup(EndpointsMapper endpointMapper, RouteGroupBuilder routeGroupBuilder)
        {
            _endpointMapper = endpointMapper;
            _routeGroupBuilder = routeGroupBuilder;
        }

        public IEndpointConventionBuilder Get(Delegate handler, int[]? versions = null)
        {
            return Get("", handler, versions);
        }

        public IEndpointConventionBuilder Get(string pattern, Delegate handler, int[]? versions = null)
        {
            return Common("Get", pattern, handler, versions);
        }

        public IEndpointConventionBuilder PostPatternHandlerName(Delegate handler, int[]? versions = null)
        {
            return Post(handler.Method.Name, handler, versions);
        }

        public IEndpointConventionBuilder Post(Delegate handler, int[]? versions = null)
        {
            return Post("", handler, versions);
        }

        public IEndpointConventionBuilder Post(string pattern, Delegate handler, int[]? versions = null)
        {
            return Common("Post", pattern, handler, versions);
        }

        public IEndpointConventionBuilder Put(Delegate handler, int[]? versions = null)
        {
            return Put("", handler, versions);
        }

        public IEndpointConventionBuilder Put(string pattern, Delegate handler, int[]? versions = null)
        {
            return Common("Put", pattern, handler, versions);
        }

        public IEndpointConventionBuilder Delete(Delegate handler, int[]? versions = null)
        {
            return Delete("", handler, versions);
        }

        public IEndpointConventionBuilder Delete(string pattern, Delegate handler, int[]? versions = null)
        {
            return Common("Delete", pattern, handler, versions);
        }

        private IEndpointConventionBuilder Common(string method, string pattern, Delegate handler, int[]? versions = null)
        {
            RouteHandlerBuilder builder;

            switch (method)
            {
                case "Get":
                    builder = _routeGroupBuilder.MapGet(pattern, handler);
                    break;
                case "Post":
                    builder = _routeGroupBuilder.MapPost(pattern, handler);
                    break;
                case "Put":
                    builder = _routeGroupBuilder.MapPut(pattern, handler);
                    break;
                case "Delete":
                    builder = _routeGroupBuilder.MapDelete(pattern, handler);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            if (!handler.Method.IsAnonymous())
            {
                builder.WithName(handler.Method.Name);

                builder.WithOpenApi();

                BuildApiVersionSet(builder, $"/{_endpointMapper.Name}/{handler.Method.Name}", versions);
            }
            else
            {
                builder.WithOpenApi();

                BuildApiVersionSet(builder, $"/{_endpointMapper.Name}/{method}", versions);
            }

            return builder;
        }

        private void BuildApiVersionSet(RouteHandlerBuilder builder, string name, int[]? versions = null)
        {
            if (versions != null && versions.Length != 0)
            {
                foreach (var version in versions)
                {
                    if (!_endpointMapper._versions.Contains(version))
                        throw new ConfigurationException($"The version {version} registered to {name} does not exist.");

                    builder.MapToApiVersion(version);
                }
            }
            else
            {
                foreach (var avaliableVersion in _endpointMapper._versions)
                    builder.MapToApiVersion(avaliableVersion);
            }
        }
    }
}

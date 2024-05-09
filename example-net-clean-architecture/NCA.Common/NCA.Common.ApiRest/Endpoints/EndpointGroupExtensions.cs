using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace NCA.Common.Api.Endpoints
{
    public static class EndpointGroupExtensions
    {
        public static RouteGroupBuilder Group(this WebApplication app, EndpointGroup group)
        {
            var apiVersionBuilder = app.NewApiVersionSet();

            foreach (var versionDescription in app.DescribeApiVersions())
                apiVersionBuilder.HasApiVersion(versionDescription.ApiVersion);

            var apiVersionSet = apiVersionBuilder.Build();

            var groupName = group.GetType().Name;

            return app.MapGroup("/api/v{version:apiVersion}/" + groupName).WithTags(groupName).WithOpenApi().WithApiVersionSet(apiVersionSet);
        }

        public static IEndpointConventionBuilder Get(this IEndpointRouteBuilder builder, Delegate handler)
        {
            return Get(builder, "", handler);
        }

        public static IEndpointConventionBuilder Get(this IEndpointRouteBuilder builder, string pattern, Delegate handler)
        {
            return handler.Method.IsAnonymous()
                ? builder.MapGet(pattern, handler).WithOpenApi()
                : (IEndpointConventionBuilder)builder.MapGet(pattern, handler).WithName(handler.Method.Name).WithOpenApi();
        }

        public static IEndpointConventionBuilder PostPatternHandlerName(this IEndpointRouteBuilder builder, Delegate handler)
        {
            return Post(builder, handler.Method.Name, handler);
        }

        public static IEndpointConventionBuilder Post(this IEndpointRouteBuilder builder, Delegate handler)
        {
            return Post(builder, "", handler);
        }

        public static IEndpointConventionBuilder Post(this IEndpointRouteBuilder builder, string pattern, Delegate handler)
        {
            return handler.Method.IsAnonymous()
                ? builder.MapPost(pattern, handler).WithOpenApi()
                : (IEndpointConventionBuilder)builder.MapPost(pattern, handler).WithName(handler.Method.Name).WithOpenApi();
        }

        public static IEndpointConventionBuilder Put(this IEndpointRouteBuilder builder, Delegate handler)
        {
            return Put(builder, "", handler);
        }

        public static IEndpointConventionBuilder Put(this IEndpointRouteBuilder builder, string pattern, Delegate handler)
        {
            return handler.Method.IsAnonymous()
                ? builder.MapPut(pattern, handler).WithOpenApi()
                : (IEndpointConventionBuilder)builder.MapPut(pattern, handler).WithName(handler.Method.Name).WithOpenApi();
        }

        public static IEndpointConventionBuilder Delete(this IEndpointRouteBuilder builder, Delegate handler)
        {
            return Delete(builder, "", handler);
        }

        public static IEndpointConventionBuilder Delete(this IEndpointRouteBuilder builder, string pattern, Delegate handler)
        {
            return handler.Method.IsAnonymous()
                ? builder.MapDelete(pattern, handler).WithOpenApi()
                : (IEndpointConventionBuilder)builder.MapDelete(pattern, handler).WithName(handler.Method.Name).WithOpenApi();
        }

        public static WebApplication MapEndpoints(this WebApplication app, Assembly assembly)
        {
            var endpointGroupType = typeof(EndpointGroup);

            var endpointGroupTypes = assembly.GetExportedTypes().Where(t => t.IsSubclassOf(endpointGroupType));

            foreach (var type in endpointGroupTypes)
            {
                if (Activator.CreateInstance(type) is EndpointGroup instance)
                {
                    instance.Map(app);
                }
            }

            return app;
        }

        public static bool IsAnonymous(this MethodInfo method)
        {
            return method.Name.Contains('<') || method.Name.Contains('>');
        }
    }
}

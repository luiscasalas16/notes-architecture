using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace NCA.Common.ApiRest.Endpoints
{
    public static class EndpointGroupExtensions
    {
        public static RouteGroupBuilder Group(this WebApplication app, EndpointGroup group, string version = "v1")
        {
            var groupName = group.GetType().Name;

            return app.MapGroup($"/api/{version}/{groupName}").WithTags(groupName).WithOpenApi();
        }

        public static IEndpointRouteBuilder Get(this IEndpointRouteBuilder builder, Delegate handler)
        {
            return Get(builder, "", handler);
        }

        public static IEndpointRouteBuilder Get(this IEndpointRouteBuilder builder, string pattern, Delegate handler)
        {
            if (handler.Method.IsAnonymous())
                builder.MapGet(pattern, handler).WithOpenApi();
            else
                builder.MapGet(pattern, handler).WithName(handler.Method.Name).WithOpenApi();

            return builder;
        }

        public static IEndpointRouteBuilder Post(this IEndpointRouteBuilder builder, Delegate handler)
        {
            return Post(builder, "", handler);
        }

        public static IEndpointRouteBuilder Post(this IEndpointRouteBuilder builder, string pattern, Delegate handler)
        {
            if (handler.Method.IsAnonymous())
                builder.MapPost(pattern, handler).WithOpenApi();
            else
                builder.MapPost(pattern, handler).WithName(handler.Method.Name).WithOpenApi();

            return builder;
        }

        public static IEndpointRouteBuilder Put(this IEndpointRouteBuilder builder, Delegate handler)
        {
            return Put(builder, "", handler);
        }

        public static IEndpointRouteBuilder Put(this IEndpointRouteBuilder builder, string pattern, Delegate handler)
        {
            if (handler.Method.IsAnonymous())
                builder.MapPut(pattern, handler).WithOpenApi();
            else
                builder.MapPut(pattern, handler).WithName(handler.Method.Name).WithOpenApi();

            return builder;
        }

        public static IEndpointRouteBuilder Delete(this IEndpointRouteBuilder builder, Delegate handler)
        {
            return Delete(builder, "", handler);
        }

        public static IEndpointRouteBuilder Delete(this IEndpointRouteBuilder builder, string pattern, Delegate handler)
        {
            if (handler.Method.IsAnonymous())
                builder.MapDelete(pattern, handler).WithOpenApi();
            else
                builder.MapDelete(pattern, handler).WithName(handler.Method.Name).WithOpenApi();

            return builder;
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
            return method.Name.Any(new[] { '<', '>' }.Contains);
        }
    }
}

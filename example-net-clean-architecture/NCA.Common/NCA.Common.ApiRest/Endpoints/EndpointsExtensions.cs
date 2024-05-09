using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace NCA.Common.Api.Endpoints
{
    public static class EndpointsExtensions
    {
        public static WebApplication MapEndpoints(this WebApplication webApplication, Assembly assembly)
        {
            var endpointMapperType = typeof(EndpointsMapper);

            var endpointMapperTypes = assembly.GetExportedTypes().Where(t => t.IsSubclassOf(endpointMapperType));

            foreach (var type in endpointMapperTypes)
            {
                var endpointMapperInstance = (EndpointsMapper)Activator.CreateInstance(type, webApplication)!;

                endpointMapperInstance.Map();
            }

            return webApplication;
        }

        public static bool IsAnonymous(this MethodInfo method)
        {
            return method.Name.Contains('<') || method.Name.Contains('>');
        }
    }
}

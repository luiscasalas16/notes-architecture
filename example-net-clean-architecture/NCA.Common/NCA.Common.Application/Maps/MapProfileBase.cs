using System.Reflection;
using AutoMapper;

namespace NCA.Common.Application.Maps
{
    public class MapProfileBase : Profile
    {
        public MapProfileBase(Assembly assembly)
        {
            ApplyMapsFromAssembly(assembly, typeof(IMapFrom<>));
            ApplyMapsFromAssembly(assembly, typeof(IMapTo<>));
        }

        private void ApplyMapsFromAssembly(Assembly assembly, Type mapType)
        {
            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().ToList().Exists(t => t.IsGenericType && t.GetGenericTypeDefinition() == mapType)).ToList();

            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Map");

                if (methodInfo != null)
                {
                    methodInfo.Invoke(instance, new object[] { this });
                }
                else
                {
                    var interfaces = type.GetInterfaces().Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == mapType).ToList();

                    if (interfaces.Count > 0)
                    {
                        foreach (var @interface in interfaces)
                        {
                            var interfaceMethodInfo = @interface.GetMethod("Map", argumentTypes);

                            interfaceMethodInfo?.Invoke(instance, new object[] { this });
                        }
                    }
                }
            }
        }
    }
}

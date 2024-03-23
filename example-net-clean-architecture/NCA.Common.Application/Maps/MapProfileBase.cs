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
            bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapType;

            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

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
                    var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                    if (interfaces.Count > 0)
                    {
                        foreach (var @interface in interfaces)
                        {
                            var interfaceMethodInfo = @interface.GetMethod("Map", argumentTypes);

                            if (interfaceMethodInfo != null)
                                interfaceMethodInfo.Invoke(instance, new object[] { this });
                        }
                    }
                }
            }
        }
    }
}

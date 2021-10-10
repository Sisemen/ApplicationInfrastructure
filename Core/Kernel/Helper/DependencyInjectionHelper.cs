using Autofac;
using Core.Kernel.Dependency;
using System.Linq;

namespace Core.Kernel.Helper
{
    public static class DependencyInjectionHelper
    {
        public static void RegisterDependencyTypes(ContainerBuilder builder)
        {
            var assemblies = AssemblyHelper.FindAssemblies("*.dll", "*.Views.dll").ToArray();

            builder.RegisterAssemblyTypes(assemblies)
                   .Where(assembly => assembly.GetInterfaces()
                                              .Any(a => a.IsAssignableFrom(typeof(IDependencyInjection))))
                   .AsImplementedInterfaces()
                   .InstancePerDependency();

            builder.RegisterAssemblyTypes(assemblies)
                   .Where(assembly => assembly.GetInterfaces()
                                              .Any(a => a.IsAssignableFrom(typeof(IPerLifetimeScopeDependencyInjection))))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblies)
                   .Where(assembly => assembly.GetInterfaces()
                                              .Any(a => a.IsAssignableFrom(typeof(ISingletonDependencyInjection))))
                   .AsImplementedInterfaces()
                   .SingleInstance();
        }
    }
}

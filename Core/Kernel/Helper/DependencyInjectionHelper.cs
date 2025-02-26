using Autofac;
using Core.Kernel.Dependency;
using System;
using System.Linq;

namespace Core.Kernel.Helper
{
    public static class DependencyInjectionHelper
    {
        public static ContainerBuilder RegisterDependencyTypes(ContainerBuilder builder, string includeCondition, string excludeCondition)
        {
            try
            {
                var assemblies = AssemblyHelper.FindAssemblies(includeCondition, excludeCondition).ToList()
                                               .ToArray();
                
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

                return builder;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}

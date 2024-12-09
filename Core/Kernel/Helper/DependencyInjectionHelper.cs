using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.Kernel.Dependency;
using System;
using System.Linq;

namespace Core.Kernel.Helper
{
    public static class DependencyInjectionHelper
    {
        public static void RegisterDependencyTypes(ContainerBuilder builder)
        {
            try
            {
                var assemblies = AssemblyHelper.FindAssemblies("RockPaperScissors.*.dll", "*.Views.dll").ToList()
                                               .Concat(AssemblyHelper.FindAssemblies("Core.Kernel.dll", string.Empty))
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}

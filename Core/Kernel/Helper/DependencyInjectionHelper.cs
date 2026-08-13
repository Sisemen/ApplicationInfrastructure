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

        // .As(t => ...) below registers a type only under the interfaces that themselves derive
        // from the marker interface -- not every interface the type implements (what
        // .AsImplementedInterfaces() would do). Without this, a type implementing a shared/base
        // interface via two different marker-derived leaf interfaces (e.g. two repositories both
        // implementing a common non-marker base) gets that shared base interface registered too,
        // ambiguously, resolving to whichever type Autofac scanned last.
        builder.RegisterAssemblyTypes(assemblies)
               .Where(assembly => assembly.GetInterfaces()
                                          .Any(a => a.IsAssignableFrom(typeof(IDependencyInjection))))
               .As(t => t.GetInterfaces().Where(i => typeof(IDependencyInjection).IsAssignableFrom(i)))
               .InstancePerDependency();

        builder.RegisterAssemblyTypes(assemblies)
               .Where(assembly => assembly.GetInterfaces()
                                          .Any(a => a.IsAssignableFrom(typeof(IPerLifetimeScopeDependencyInjection))))
               .As(t => t.GetInterfaces().Where(i => typeof(IPerLifetimeScopeDependencyInjection).IsAssignableFrom(i)))
               .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(assemblies)
               .Where(assembly => assembly.GetInterfaces()
                                          .Any(a => a.IsAssignableFrom(typeof(ISingletonDependencyInjection))))
               .As(t => t.GetInterfaces().Where(i => typeof(ISingletonDependencyInjection).IsAssignableFrom(i)))
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

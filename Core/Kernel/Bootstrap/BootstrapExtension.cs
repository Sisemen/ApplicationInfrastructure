using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Core.Kernel.Helper;

namespace Core.Kernel.Bootstrap
{
    public static class BootstrapExtension
    {
        public static void RegisterDependencies(this ContainerBuilder builder)
        {
            DependencyInjectionHelper.RegisterDependencyTypes(builder);

            //ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(applicationContainer));
        }

        public static void SetServiceLocator(this ILifetimeScope applicationContainer)
        {
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(applicationContainer));
        }
    }
}

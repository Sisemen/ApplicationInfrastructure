using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Core.Kernel.Helper;

namespace Core.Kernel.Bootstrap
{
    public static class BootstrapExtension
    {
        public static ContainerBuilder RegisterKernelDependencies(this ContainerBuilder builder) =>
            DependencyInjectionHelper.RegisterDependencyTypes(builder, "Core.Kernel.dll", string.Empty);

        public static void SetServiceLocator(this ILifetimeScope applicationContainer)
        {
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(applicationContainer));
        }
    }
}

using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Core.Kernel.Helper;
using System;

namespace Core.Kernel.Bootstrap
{
    public class Bootstrap
    {
        protected ILifetimeScope ApplicationContainer { get; set; }

        protected virtual void RegisterDependencies(ContainerBuilder builder)
        {
            throw new NotImplementedException();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            RegisterDependencies(builder);

            DependencyInjectionHelper.RegisterDependencyTypes(builder);

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(ApplicationContainer));
        }
    }
}

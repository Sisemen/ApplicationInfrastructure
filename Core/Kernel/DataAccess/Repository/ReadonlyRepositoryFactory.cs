using Autofac;

namespace Core.Kernel.DataAccess.Repository
{
    public class ReadonlyRepositoryFactory : IReadonlyRepositoryFactory
    {
        private readonly ILifetimeScope _scope;

        public ReadonlyRepositoryFactory(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public IReadonlyRepository Create()
        {
            return _scope.Resolve<IReadonlyRepository>();
        }
    }
}

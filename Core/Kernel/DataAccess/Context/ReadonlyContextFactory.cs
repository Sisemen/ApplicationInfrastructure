using Autofac;
using System;

namespace Core.Kernel.DataAccess.Context
{
    public class ReadonlyContextFactory: IReadonlyContextFactory
    {
        private readonly ILifetimeScope _scope;
        private IReadonlyContext _contextReadonly = null!;

        public ReadonlyContextFactory(ILifetimeScope scope)
        {
            _scope = scope;
        }
      
        public IReadonlyContext Create()
        {
            _contextReadonly = _scope.Resolve<IReadonlyContext>();

            return _contextReadonly;
        }
    }
}

using Autofac;
using System;

namespace Core.Kernel.DataAccess.Context
{
    public class ReadonlyContextFactory: IReadonlyContextFactory, IDisposable
    {
        private readonly ILifetimeScope _scope;
        private IReadonlyContext _contextReadonly;

        public ReadonlyContextFactory(ILifetimeScope scope)
        {
            _scope = scope;
        }
      
        public IReadonlyContext Create()
        {
            _contextReadonly = _scope.Resolve<IReadonlyContext>();

            return _contextReadonly;
        }

        public void Dispose()
        {
            _contextReadonly?.Dispose();
            _contextReadonly = null;
        }
    }
}

using System;

namespace Core.Kernel.DataAccess.Context
{
    internal class ContextFactory : IContextFactory, IDisposable
    {
        private IContext CurrentContext { get; set; }

        public ContextFactory()
        {

        }

        public IContext Create()
        {
            return CurrentContext ??= CommonServiceLocator.ServiceLocator.Current.GetInstance<IContext>();
        }

        public void Dispose()
        {
            CurrentContext.Dispose();
            CurrentContext = null;
        }
    }
}

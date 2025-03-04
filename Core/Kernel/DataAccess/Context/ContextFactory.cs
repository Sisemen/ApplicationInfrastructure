using System;

namespace Core.Kernel.DataAccess.Context
{
    internal class ContextFactory : IContextFactory
    {
        private IContext CurrentContext { get; set; } = null!;

        public ContextFactory()
        {

        }

        public IContext Create()
        {
            return CurrentContext ??= CommonServiceLocator.ServiceLocator.Current.GetInstance<IContext>();
        }
    }
}

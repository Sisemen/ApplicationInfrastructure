using Core.Kernel.Dependency;

namespace Core.Kernel.DataAccess.Context
{
    internal interface IContextFactory : IDependencyInjection
    {
        IContext Create();
    }
}

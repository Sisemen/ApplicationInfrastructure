using Core.Kernel.Dependency;

namespace Core.Kernel.DataAccess.Context
{
    public interface IReadonlyContextFactory : IDependencyInjection
    {
        IReadonlyContext Create();
        void Dispose();
    }
}
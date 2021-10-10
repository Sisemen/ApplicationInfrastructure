using Core.Kernel.Dependency;

namespace Core.Kernel.DataAccess.Repository
{
    public interface IReadonlyRepositoryFactory : IDependencyInjection
    {
        IReadonlyRepository Create();
    }
}
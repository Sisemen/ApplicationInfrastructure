using Core.Kernel.Dependency;
using System;
using System.Threading.Tasks;

namespace Core.Kernel.DataAccess.Context
{
    public interface IUnitOfWork : IPerLifetimeScopeDependencyInjection, IDisposable
    {
        Task CommitAsync();
        void Rollback();
    }
}

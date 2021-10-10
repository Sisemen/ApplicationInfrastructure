using Core.Kernel.DataAccess.Model;
using Core.Kernel.Dependency;
using System.Linq;

namespace Core.Kernel.DataAccess.Context
{
    public interface IReadonlyContext : IDependencyInjection
    {
        IQueryable<T> AsQueryable<T>() where T : class, IEntity;
        void Dispose();
    }
}

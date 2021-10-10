using Core.Kernel.DataAccess.Model;
using Core.Kernel.Dependency;
using System.Linq;

namespace Core.Kernel.DataAccess.Repository
{
    public interface IReadonlyRepository : IDependencyInjection
    {
        IQueryable<T> Query<T>() where T : class, IEntity;
    }
}

using Core.Kernel.DataAccess.Model;
using Core.Kernel.Dependency;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Kernel.DataAccess.Context
{
    public interface IContext : IPerLifetimeScopeDependencyInjection
    {
        Task<T> InsertAsync<T>(T entity) where T : class, IEntity;
        Task InsertAsync<T>(IEnumerable<T> entities) where T : class, IEntity;
        bool Update<T>(T entity) where T : class, IEntity;
        void Update<T>(IEnumerable<T> entities) where T : class, IEntity;
        bool Delete<T>(object id) where T : class, IEntity;
        void Delete<T>(IEnumerable<T> entities) where T : class, IEntity;
        IQueryable<T> Query<T>(bool noTracking) where T : class, IEntity;
        Task<T> GetAsync<T>(object id) where T : class, IEntity;
        Task ApplyChangesAsync();

        void Dispose();
    }
}

using Core.Kernel.DataAccess.Model;
using Core.Kernel.Dependency;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Kernel.DataAccess.Repository
{
    public interface IRepository : IDependencyInjection
    {
        IQueryable<T> ToQueryable<T>(bool noTracking = true) where T : class, IEntity;
        Task<T> GetAsync<T>(object id) where T : class, IEntity;
        Task<T> CreateAsync<T>(T entity) where T : class, IEntity;
        Task CreateAsync<T>(IEnumerable<T> entities) where T : class, IEntity;
        bool Delete<T>(object id) where T : class, IEntity;
        void Delete<T>(IEnumerable<T> entities) where T : class, IEntity;
        Task<bool> UpdateAsync<T>(T entity) where T : class, IEntity;
        void Update<T>(IEnumerable<T> entities) where T : class, IEntity;
    }
}
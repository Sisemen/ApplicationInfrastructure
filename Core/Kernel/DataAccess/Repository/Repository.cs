using Core.Kernel.DataAccess.Context;
using Core.Kernel.DataAccess.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Kernel.DataAccess.Repository
{
    public class Repository : IRepository
    {
        private readonly IContext _context;

        public Repository(IContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync<T>(T entity) where T : class, IEntity => await _context.InsertAsync(entity);
        public async Task CreateAsync<T>(IEnumerable<T> entities) where T : class, IEntity => await _context.InsertAsync(entities);
        public bool Delete<T>(object id) where T : class, IEntity => _context.Delete<T>(id);
        public void Delete<T>(IEnumerable<T> entities) where T : class, IEntity => _context.Delete(entities);
        public async Task<bool> UpdateAsync<T>(T entity) where T : class, IEntity => await _context.UpdateAsync(entity);
        public void Update<T>(IEnumerable<T> entities) where T : class, IEntity => _context.Update(entities);
        public async Task<T> GetAsync<T>(object id) where T : class, IEntity => await _context.GetAsync<T>(id);
        public IQueryable<T> ToQueryable<T>(bool noTracking = true) where T : class, IEntity => _context.Query<T>(noTracking);
    }
}
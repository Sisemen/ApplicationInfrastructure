using Core.Kernel.DataAccess.Context;
using Core.Kernel.DataAccess.Model;
using System.Linq;

namespace Core.Kernel.DataAccess.Repository
{
    public class ReadonlyRepository : IReadonlyRepository
    {
        private readonly IReadonlyContext _contextReadonly;

        public ReadonlyRepository(IReadonlyContextFactory contextFactory)
        {
            _contextReadonly = contextFactory.Create();
        }

        public IQueryable<T> Query<T>() where T : class, IEntity
        {
            return _contextReadonly.AsQueryable<T>();
        }
    }
}

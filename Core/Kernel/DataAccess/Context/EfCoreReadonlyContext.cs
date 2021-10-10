using Core.Kernel.DataAccess.Model;
using Core.Kernel.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Core.Kernel.DataAccess.Context
{
    public class EfCoreReadonlyContext : DbContext, IReadonlyContext
    {
        private readonly IContextHelper _contextHelper;

        public EfCoreReadonlyContext(DbContextOptions<EfCoreReadonlyContext> options, IContextHelper contextHelper)
            : base(options)
        {
            _contextHelper = contextHelper;

            base.Database.AutoTransactionsEnabled = true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _contextHelper.ApplyModelCreating(modelBuilder);
        }

        public IQueryable<T> AsQueryable<T>() where T : class, IEntity
        {
            return base.Set<T>().AsQueryable().AsNoTracking();
        }

        public new void Dispose()
        {
            base.Dispose();
        }
    }
}
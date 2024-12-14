using System.Threading.Tasks;

namespace Core.Kernel.DataAccess.Context
{
    public class UnitOfWork(IContext context) : IUnitOfWork
    {
        public async Task CommitAsync()
        {
            await context.ApplyChangesAsync();
        }

        public void Rollback()
        {
            context.Rollback();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}

using Core.Kernel.Dependency;
using Microsoft.EntityFrameworkCore;

namespace Core.Kernel.Helper
{
    public interface IContextHelper : IPerLifetimeScopeDependencyInjection
    {
        void ApplyModelCreating(ModelBuilder modelBuilder);
    }
}

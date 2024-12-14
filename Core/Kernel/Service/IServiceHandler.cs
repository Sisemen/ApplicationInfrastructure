using Core.Kernel.Dependency;
using System.Threading.Tasks;
using System;

namespace Core.Kernel.Service
{
    public interface IServiceHandler : IPerLifetimeScopeDependencyInjection
    {
        Task<IDto?> HandleAsync<TResponse>(Func<Task<IDto?>> serviceHandlerDelegate);
    }
}

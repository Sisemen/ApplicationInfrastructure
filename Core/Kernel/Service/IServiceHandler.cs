using Core.Kernel.Dependency;
using System.Threading.Tasks;
using System;

namespace Core.Kernel.Service
{
    public interface IServiceHandler : IPerLifetimeScopeDependencyInjection
    {
        Task<IServiceResponse<IDto?>> HandleAsync<TResponse>(Func<Task<IDto?>> serviceHandlerDelegate);
    }
}

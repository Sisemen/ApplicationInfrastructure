using Core.Kernel.Dependency;
using System;
using System.Threading.Tasks;

namespace Core.Kernel.Service
{
    public interface IService<TRequest, TResponse> : IPerLifetimeScopeDependencyInjection where TResponse : IDto? where TRequest : IDto?
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}

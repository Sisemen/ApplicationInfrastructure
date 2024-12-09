using System.Threading.Tasks;

namespace Core.Kernel.Service
{
    public interface IService<TRequest, TResponse> where TResponse : IDto, new() where TRequest : IDto, new()
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}

using System.Threading.Tasks;

namespace Core.Kernel.Service
{
    public interface IService<TResponse, TRequest> where TResponse : IDto, new() where TRequest : IDto, new()
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}

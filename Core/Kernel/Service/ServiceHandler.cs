using Core.Kernel.DataAccess.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Core.Kernel.Service
{
    public class ServiceHandler(IUnitOfWork unitOfWork, ILogger<ServiceHandler> logger) : IServiceHandler
    {
        public async Task<IDto?> HandleAsync<TResponse>(Func<Task<IDto?>> serviceHandlerDelegate)
        {
            try
            {
                var response = await serviceHandlerDelegate.Invoke();

                await unitOfWork.CommitAsync();

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Handling service method has been failed. Service: {serviceHandlerDelegate.Target}; MethodName: {serviceHandlerDelegate.Method.Name}");

                unitOfWork.Rollback();

                return null;
            }
        }
    }
}

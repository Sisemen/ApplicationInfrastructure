using Core.Kernel.DataAccess.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Core.Kernel.Service
{
    public class ServiceHandler(IUnitOfWork unitOfWork, ILogger<ServiceHandler> logger) : IServiceHandler
    {
        public async Task<IServiceResponse<IDto?>> HandleAsync<TResponse>(Func<Task<IDto?>> serviceHandlerDelegate)
        {
            try
            {
                var response = await serviceHandlerDelegate.Invoke();

                await unitOfWork.CommitAsync();

                return new ServiceResponse<IDto?>
                {
                    Data = response,
                    Message = string.Empty,
                    StatusCode = ResponseStatusCode.Success
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Handling service method has been failed. Service: {serviceHandlerDelegate.Target}; MethodName: {serviceHandlerDelegate.Method.Name}");

                unitOfWork.Rollback();

                return new ServiceResponse<IDto?>
                {
                    Data = null,
                    Message = "Handling service method has been failed.",
                    StatusCode = ResponseStatusCode.Failed
                };
            }
        }

        public async Task<IServiceResponse<TResponse>> HandleAsync<TResponse>(Func<Task<TResponse>> serviceHandlerDelegate) where TResponse : IDto
        {
            try
            {
                var response = await serviceHandlerDelegate.Invoke();

                await unitOfWork.CommitAsync();

                return new ServiceResponse<TResponse>
                {
                    Data = response,
                    Message = string.Empty,
                    StatusCode = ResponseStatusCode.Success
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Handling service method has been failed. Service: {serviceHandlerDelegate.Target}; MethodName: {serviceHandlerDelegate.Method.Name}");

                unitOfWork.Rollback();

                return new ServiceResponse<TResponse>
                {
                    Data = default,
                    Message = "Handling service method has been failed.",
                    StatusCode = ResponseStatusCode.Failed
                };
            }
        }
    }
}

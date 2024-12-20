namespace Core.Kernel.Service
{
    public class ServiceResponse<T> : IServiceResponse<T> where T : IDto?
    {
        public T? Data { get; set; }
        public ResponseStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
    }
}

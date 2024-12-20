namespace Core.Kernel.Service
{
    public interface IServiceResponse<T> where T : IDto?
    {
        T? Data { get; set; }
        ResponseStatusCode StatusCode { get; set; }
        string? Message { get; set; }
    }
}

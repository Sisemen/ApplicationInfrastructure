namespace Core.Kernel.Service
{
    public interface IServiceResponse<T>
    {
        T Value { get; set; }
    }
}

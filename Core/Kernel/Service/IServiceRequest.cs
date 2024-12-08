namespace Core.Kernel.Service
{
    public interface IServiceRequest<T>
    {
        T Value { get; set; }
    }
}

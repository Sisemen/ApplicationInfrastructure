namespace Core.Kernel.Service
{
    public class ServiceWrapper(IServiceHandler serviceHandler) : IServiceWrapper
    {
        public IServiceHandler ServiceHandler { get => serviceHandler; } 
    }
}

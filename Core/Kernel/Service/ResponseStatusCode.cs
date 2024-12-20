namespace Core.Kernel.Service
{
    public enum ResponseStatusCode
    {
        Success = 0,
        Failed = 1,
        InvalidInput = 3,
        LoginFailed = 4,
        Unauthorized = 401,
        Forbidden = 403
    }
}

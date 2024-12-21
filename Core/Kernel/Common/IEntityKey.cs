namespace Core.Kernel.Common
{
    public interface IEntityKey<T> where T : struct
    {
        T Value { get; set; }
    }
}

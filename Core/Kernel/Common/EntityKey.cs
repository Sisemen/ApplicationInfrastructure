namespace Core.Kernel.Common
{
    public class EntityKey<T> : IEntityKey<T> where T : struct
    {
        public T Value { get; set; }
    }
}

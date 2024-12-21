using Core.Kernel.Common;
using System.Text.Json.Serialization;

namespace Core.Kernel.Service
{
    public class DtoBase<T> : IDto where T : struct
    {
        [JsonIgnore]
        public EntityKey<T>? Key { get; set; } = null;
        public T Id
        {
            get => Key?.Value ?? default;
            set
            {
                if (Key is null)
                {
                    Key = new EntityKey<T> { Value = value };
                }
                else
                {
                    Key.Value = value;
                }
            }
        }
    }
}

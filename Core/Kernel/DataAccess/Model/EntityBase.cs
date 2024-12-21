using Core.Kernel.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Kernel.DataAccess.Model
{
    public abstract class EntityBase<T> : IEntity where T : struct
    {
        [NotMapped]
        public EntityKey<T>? Key { get; set; } = null;

        [Key]
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

        public byte[] Version { get; private set; } = null!;

        byte[] IEntity.Version
        {
            get => this.Version;
            set => this.Version = value;
        }

        [NotMapped]
        public T TemporaryId { get; set; }
        [NotMapped]
        public abstract Action<ModelBuilder> OnConfiguringEntity { get; }
    }
}
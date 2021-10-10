using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Kernel.DataAccess.Model
{
    public abstract class Entity<T> : IEntity
    {
        [Key]
        public T Id { get; set; }
        public byte[] Version { get; private set; }

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
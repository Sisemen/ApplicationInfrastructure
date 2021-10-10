using Microsoft.EntityFrameworkCore;
using System;

namespace Core.Kernel.DataAccess.Model
{
    public interface IEntity
    {
        byte[] Version { get; set; }

        Action<ModelBuilder> OnConfiguringEntity { get; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;

namespace Core.Kernel.DataAccess.Model
{
    public interface IEntity
    {
#if USE_POSTGRES
        uint Version { get; set; }
#else
        byte[] Version { get; set; }
#endif

        Action<ModelBuilder> OnConfiguringEntity { get; }
    }
}

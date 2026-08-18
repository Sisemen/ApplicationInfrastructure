using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq.Expressions;

namespace Core.Kernel.DataAccess.Model
{
    public static class ConcurrencyTokenExtensions
    {
#if USE_POSTGRES
        public static void ConfigureVersionAsConcurrencyToken<TEntity>(
            this EntityTypeBuilder<TEntity> entityType,
            Expression<Func<TEntity, uint>> versionSelector)
            where TEntity : class
        {
            entityType.Property(versionSelector)
                .IsRowVersion();
        }
#else
        public static void ConfigureVersionAsConcurrencyToken<TEntity>(
            this EntityTypeBuilder<TEntity> entityType,
            Expression<Func<TEntity, byte[]>> versionSelector)
            where TEntity : class
        {
            entityType.Property(versionSelector)
                .HasColumnType("timestamp")
                .IsRowVersion();
        }
#endif
    }
}

﻿using Core.Kernel.Dependency;
using System;
using System.Threading.Tasks;

namespace Core.Kernel.DataAccess.Context
{
    public interface IUnitOfWork : IPerLifetimeScopeDependencyInjection
    {
        Task CommitAsync();
        void Rollback();
    }
}

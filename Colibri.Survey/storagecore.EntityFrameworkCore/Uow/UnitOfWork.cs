using Microsoft.EntityFrameworkCore;
using storagecore.Abstractions.Uow;
using System;
using System.Collections.Generic;
using System.Text;

namespace storagecore.EntityFrameworkCore.Uow
{
    public class UnitOfWork : UnitOfWorkBase<DbContext>, IUnitOfWork
    {
        public UnitOfWork(DbContext context, IServiceProvider provider) : base(context, provider)
        { }
    }
}

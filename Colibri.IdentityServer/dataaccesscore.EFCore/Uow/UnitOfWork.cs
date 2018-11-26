using dataaccesscore.Abstractions.Uow;
using Microsoft.EntityFrameworkCore;
using System;

namespace dataaccesscore.EFCore.Uow
{
    public class UnitOfWork : UnitOfWorkBase<DbContext>, IUnitOfWork
    {
        public UnitOfWork(DbContext context, IServiceProvider provider) : base(context, provider)
        { }
    }
}

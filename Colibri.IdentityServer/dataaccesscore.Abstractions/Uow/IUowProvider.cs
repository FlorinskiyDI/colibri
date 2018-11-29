using System;
using System.Collections.Generic;
using System.Text;

namespace dataaccesscore.Abstractions.Uow
{
    public interface IUowProvider
    {
        IUnitOfWork CreateUnitOfWork(bool trackChanges = true, bool enableLogging = false);
        IUnitOfWork CreateUnitOfWork<TEntityContext>(bool trackChanges = true, bool enableLogging = false) where TEntityContext : IDisposable;
    }
}

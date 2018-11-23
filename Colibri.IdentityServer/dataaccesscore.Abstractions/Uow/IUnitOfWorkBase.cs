using dataaccesscore.Abstractions.Entities;
using dataaccesscore.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dataaccesscore.Abstractions.Uow
{
    public interface IUnitOfWorkBase : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IBaseRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : IBaseEntity<TKey>;
        TRepository GetCustomRepository<TRepository>();
    }
}

using storagecore.Abstractions.Entities;
using storagecore.Abstractions.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace storagecore.Abstractions.Uow
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

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

        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        TRepository GetCustomRepository<TRepository>();
    }
}

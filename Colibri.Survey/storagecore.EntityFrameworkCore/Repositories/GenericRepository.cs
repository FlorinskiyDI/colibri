using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using storagecore.EntityFrameworkCore.Entities;
using storagecore.EntityFrameworkCore.Models;

namespace storagecore.EntityFrameworkCore.Repositories
{
    public class GenericRepository<TEntity, TKey> : BaseRepository<DbContext, TEntity, TKey>
       where TEntity : BaseEntity<TKey>, new()
    {
        public GenericRepository(ILogger<LoggerDataAccess> logger) : base(logger, null)
        {

        }
    }
}

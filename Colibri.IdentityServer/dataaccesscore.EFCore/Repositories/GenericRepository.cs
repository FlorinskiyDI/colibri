using dataaccesscore.EFCore.Entities;
using dataaccesscore.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace dataaccesscore.EFCore.Repositories
{
    public class GenericRepository<TEntity, TKey> : BaseRepository<DbContext, TEntity, TKey>
       where TEntity : BaseEntity<TKey>, new()
    {
        public GenericRepository(ILogger<LoggerDataAccess> logger) : base(logger, null)
        {

        }
    }
}

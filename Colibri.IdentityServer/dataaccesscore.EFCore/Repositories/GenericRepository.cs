using dataaccesscore.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace dataaccesscore.EFCore.Repositories
{
    public class GenericRepository<TEntity> : BaseRepository<DbContext, TEntity>
       where TEntity : class, new()
    {
        public GenericRepository(ILogger<LoggerDataAccess> logger) : base(logger, null)
        {

        }
    }
}

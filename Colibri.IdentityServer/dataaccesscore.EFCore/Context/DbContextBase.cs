using dataaccesscore.Abstractions.Context;
using Microsoft.EntityFrameworkCore;

namespace dataaccesscore.EFCore.Context
{
    public class DbContextBase<TContext> : DbContext, IEntityContext where TContext : DbContext
    {
        public DbContextBase(DbContextOptions<TContext> options) : base(options)
        {
        }
    }
}

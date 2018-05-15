using Microsoft.EntityFrameworkCore;
using storagecore.Abstractions.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace storagecore.EntityFrameworkCore.Context
{
    public class DbContextBase<TContext> : DbContext, IEntityContext where TContext : DbContext
    {
        public DbContextBase(DbContextOptions<TContext> options) : base(options)
        {
        }
    }
}

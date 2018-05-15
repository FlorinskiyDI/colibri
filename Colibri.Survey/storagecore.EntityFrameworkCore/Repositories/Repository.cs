using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using storagecore.Abstractions.Repositories;
using storagecore.EntityFrameworkCore.Models;

namespace storagecore.EntityFrameworkCore.Repositories
{
    public abstract class Repository<TContext> : IRepositoryInjection where TContext : DbContext
    {
        protected Repository(ILogger<LoggerDataAccess> logger, TContext context)
        {
            this.Logger = logger;
            this.Context = context;
        }

        protected ILogger Logger { get; private set; }
        protected TContext Context { get; private set; }

        public IRepositoryInjection SetContext(DbContext context)
        {
            this.Context = (TContext)context;
            return this;
        }

        //IRepositoryInjection<TContext> IRepositoryInjection<TContext>.SetContext(TContext context)
        //{
        //    this.Context = context;
        //    return this;
        //}
    }
}

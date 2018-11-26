using dataaccesscore.Abstractions.Repositories;
using dataaccesscore.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace dataaccesscore.EFCore.Repositories
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

        public IRepositoryInjection SetContext(IDisposable context)
        {
            this.Context = (TContext)context;
            return this;
        }

    }
}

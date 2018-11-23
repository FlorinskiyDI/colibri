using dataaccesscore.Abstractions.Context;
using dataaccesscore.Abstractions.Uow;
using dataaccesscore.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace dataaccesscore.EFCore.Uow
{
    public class UowProvider : IUowProvider
    {

        public UowProvider()
        { }

        public UowProvider(ILogger<LoggerDataAccess> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public IUnitOfWork CreateUnitOfWork(bool trackChanges = true, bool enableLogging = false)
        {

            var _context = (DbContext)_serviceProvider.GetService(typeof(IEntityContext));

            if (!trackChanges)
                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var uow = new UnitOfWork(_context, _serviceProvider);
            return uow;
        }

        public IUnitOfWork CreateUnitOfWork<TEntityContext>(bool trackChanges = true, bool enableLogging = false) where TEntityContext : IDisposable
        {
            var _context = (DbContext)_serviceProvider.GetService(typeof(IEntityContext));

            if (!trackChanges)
                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var uow = new UnitOfWork(_context, _serviceProvider);
            return uow;
        }
    }
}

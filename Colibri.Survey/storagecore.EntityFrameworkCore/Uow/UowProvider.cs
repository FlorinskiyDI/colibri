using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using storagecore.Abstractions.Context;
using storagecore.Abstractions.Uow;
using System;
using System.Collections.Generic;
using System.Text;
using storagecore.EntityFrameworkCore.Models;

namespace storagecore.EntityFrameworkCore.Uow
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

        public IUnitOfWork CreateUnitOfWork<TEntityContext>(bool trackChanges = true, bool enableLogging = false) where TEntityContext : DbContext
        {
            var _context = (TEntityContext)_serviceProvider.GetService(typeof(IEntityContext));

            if (!trackChanges)
                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var uow = new UnitOfWork(_context, _serviceProvider);
            return uow;
        }
    }
}
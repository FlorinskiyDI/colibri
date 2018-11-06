using Survey.InfrastructureLayer.Context;
using System;
using Survey.DomainModelLayer.Entities;
using Survey.DomainModelLayer.Contracts.Repositories;
using Microsoft.Extensions.Logging;
using dataaccesscore.EFCore.Repositories;
using dataaccesscore.EFCore.Models;


namespace Survey.InfrastructureLayer.Repositories
{

    class PageRepository : BaseRepository<ApplicationDbContext, Pages, Guid>, IPageRepository
    {
        public PageRepository(ILogger<LoggerDataAccess> logger)
            : base(logger, null)
        {
        }
    }
}

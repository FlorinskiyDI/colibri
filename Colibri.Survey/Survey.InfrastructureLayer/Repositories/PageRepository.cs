using storagecore.EntityFrameworkCore.Repositories;
using Survey.InfrastructureLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Survey.DomainModelLayer.Entities;
using Survey.DomainModelLayer.Contracts.Repositories;
using Microsoft.Extensions.Logging;
using storagecore.EntityFrameworkCore.Models;

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

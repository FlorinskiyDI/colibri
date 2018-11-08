using Microsoft.Extensions.Logging;
using Survey.DomainModelLayer.Contracts.Repositories;
using Survey.DomainModelLayer.Entities;
using Survey.InfrastructureLayer.Context;
using System;
using dataaccesscore.EFCore.Repositories;
using dataaccesscore.EFCore.Models;

namespace Survey.InfrastructureLayer.Repositories
{
    class SurveySectionRepository : BaseRepository<ApplicationDbContext, SurveySections, Guid>, ISurveySectionRepository
    {
        public SurveySectionRepository(ILogger<LoggerDataAccess> logger)
            : base(logger, null)
        {
        }
    }
}

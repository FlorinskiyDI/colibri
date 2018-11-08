using Microsoft.Extensions.Logging;
using Survey.InfrastructureLayer.Context;
using System;
using Survey.DomainModelLayer.Entities;
using Survey.DomainModelLayer.Contracts.Repositories;
using dataaccesscore.EFCore.Repositories;
using dataaccesscore.EFCore.Models;

namespace Survey.InfrastructureLayer.Repositories
{

    class OptionGroupRepository : BaseRepository<ApplicationDbContext, OptionGroups, Guid>, IOptionGroupRepository
    {
        public OptionGroupRepository(ILogger<LoggerDataAccess> logger)
            : base(logger, null)
        {
        }
    }
}

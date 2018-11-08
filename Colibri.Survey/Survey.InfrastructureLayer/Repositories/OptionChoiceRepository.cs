using Microsoft.Extensions.Logging;
using Survey.InfrastructureLayer.Context;
using System;
using Survey.DomainModelLayer.Entities;
using Survey.DomainModelLayer.Contracts.Repositories;
using dataaccesscore.EFCore.Repositories;
using dataaccesscore.EFCore.Models;

namespace Survey.InfrastructureLayer.Repositories
{

    class OptionChoiceRepository : BaseRepository<ApplicationDbContext, OptionChoises, Guid>, IOptionChoiceRepository
    {
        public OptionChoiceRepository(ILogger<LoggerDataAccess> logger)
            : base(logger, null)
        {
        }
    }
}

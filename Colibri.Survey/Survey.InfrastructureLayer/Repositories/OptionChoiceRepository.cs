using Microsoft.Extensions.Logging;
using storagecore.EntityFrameworkCore.Models;
using storagecore.EntityFrameworkCore.Repositories;
using Survey.InfrastructureLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Survey.DomainModelLayer.Entities;
using Survey.DomainModelLayer.Contracts.Repositories;

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

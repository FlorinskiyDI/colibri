using Microsoft.Extensions.Logging;
using Survey.DomainModelLayer.Contracts.Repositories;
using Survey.DomainModelLayer.Entities;
using Survey.InfrastructureLayer.Context;
using System;
using dataaccesscore.EFCore.Repositories;
using dataaccesscore.EFCore.Models;


namespace Survey.InfrastructureLayer.Repositories
{

    class RespondentRepository : BaseRepository<ApplicationDbContext, Respondents, Guid>, IRespondentRepository
    {
        public RespondentRepository(ILogger<LoggerDataAccess> logger)
            : base(logger, null)
        {
        }
    }
}

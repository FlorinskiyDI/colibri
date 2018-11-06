using dataaccesscore.EFCore.Models;
using dataaccesscore.EFCore.Repositories;
using Microsoft.Extensions.Logging;
using Survey.DomainModelLayer.Contracts.Repositories;
using Survey.DomainModelLayer.Entities;
using Survey.InfrastructureLayer.Context;
using System;

namespace Survey.InfrastructureLayer.Repositories
{

    class AnswerRepository : BaseRepository<ApplicationDbContext, Answers, Guid>, IAnswerRepository
    {
        public AnswerRepository(ILogger<LoggerDataAccess> logger)
            : base(logger, null)
        {
        }
    }
}

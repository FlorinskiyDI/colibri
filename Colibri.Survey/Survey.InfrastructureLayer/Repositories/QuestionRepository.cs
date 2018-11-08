using Survey.InfrastructureLayer.Context;
using System;
using Microsoft.Extensions.Logging;
using Survey.DomainModelLayer.Contracts.Repositories;
using Survey.DomainModelLayer.Entities;
using dataaccesscore.EFCore.Repositories;
using dataaccesscore.EFCore.Models;

namespace Survey.InfrastructureLayer.Repositories
{

    class QuestionRepository : BaseRepository<ApplicationDbContext, Questions, Guid>, IQuestionRepository
    {
        public QuestionRepository(ILogger<LoggerDataAccess> logger)
            : base(logger, null)
        {
        }
    }
}

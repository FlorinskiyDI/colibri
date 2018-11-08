using Survey.DomainModelLayer.Contracts.Repositories;
using System;
using Microsoft.Extensions.Logging;
using Survey.DomainModelLayer.Entities;
using Survey.InfrastructureLayer.Context;
using dataaccesscore.EFCore.Repositories;
using dataaccesscore.EFCore.Models;

namespace Survey.InfrastructureLayer.Repositories
{

    class QuestionOptionRepository : BaseRepository<ApplicationDbContext, QuestionOptions, Guid>, IQuestionOptionRepository
    {
        public QuestionOptionRepository(ILogger<LoggerDataAccess> logger)
            : base(logger, null)
        {
        }
    }
}

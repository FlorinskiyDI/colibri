using storagecore.EntityFrameworkCore.Repositories;
using Survey.DomainModelLayer.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using storagecore.EntityFrameworkCore.Models;
using Survey.DomainModelLayer.Entities;
using Survey.InfrastructureLayer.Context;

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

using storagecore.EntityFrameworkCore.Repositories;
using Survey.InfrastructureLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using storagecore.EntityFrameworkCore.Models;
using Survey.DomainModelLayer.Contracts.Repositories;
using Survey.DomainModelLayer.Entities;

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

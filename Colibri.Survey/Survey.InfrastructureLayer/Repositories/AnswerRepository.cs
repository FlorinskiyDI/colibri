using Microsoft.Extensions.Logging;
using storagecore.EntityFrameworkCore.Models;
using storagecore.EntityFrameworkCore.Repositories;
using Survey.DomainModelLayer.Contracts.Repositories;
using Survey.DomainModelLayer.Entities;
using Survey.InfrastructureLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;

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

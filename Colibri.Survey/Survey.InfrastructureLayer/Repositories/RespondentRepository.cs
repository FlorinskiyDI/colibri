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

    class RespondentRepository : BaseRepository<ApplicationDbContext, Respondents, Guid>, IRespondentRepository
    {
        public RespondentRepository(ILogger<LoggerDataAccess> logger)
            : base(logger, null)
        {
        }
    }
}

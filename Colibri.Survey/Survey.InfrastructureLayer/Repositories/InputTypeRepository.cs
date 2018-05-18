using Microsoft.Extensions.Logging;
using storagecore.EntityFrameworkCore.Models;
using storagecore.EntityFrameworkCore.Repositories;
using Survey.InfrastructureLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Survey.DomainModelLayer.Contracts.Repositories;
using Survey.DomainModelLayer.Entities;

namespace Survey.InfrastructureLayer.Repositories
{
    public class InputTypeRepository : BaseRepository<ApplicationDbContext, InputTypes, Guid>, IInputTypeRepository
    {
        public InputTypeRepository(ILogger<LoggerDataAccess> logger)
            : base(logger, null)
        {
        }
    }
}

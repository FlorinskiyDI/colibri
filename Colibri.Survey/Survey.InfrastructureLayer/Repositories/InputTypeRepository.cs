using Microsoft.Extensions.Logging;
using Survey.InfrastructureLayer.Context;
using System;
using Survey.DomainModelLayer.Contracts.Repositories;
using Survey.DomainModelLayer.Entities;
using dataaccesscore.EFCore.Repositories;
using dataaccesscore.EFCore.Models;

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

using dataaccesscore.Abstractions.Repositories;
using Survey.DomainModelLayer.Entities;
using System;

namespace Survey.DomainModelLayer.Contracts.Repositories
{
    public interface ISurveySectionRepository : IBaseRepository<SurveySections, Guid>
    {
    }
}

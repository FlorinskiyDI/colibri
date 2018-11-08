using System;
using dataaccesscore.Abstractions.Repositories;
using Survey.DomainModelLayer.Entities;

namespace Survey.DomainModelLayer.Contracts.Repositories
{

    public interface IOptionGroupRepository : IBaseRepository<OptionGroups, Guid>
    {
    }
}

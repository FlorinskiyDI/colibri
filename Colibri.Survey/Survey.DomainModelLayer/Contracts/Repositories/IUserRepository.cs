using storagecore.Abstractions.Repositories;
using Survey.DomainModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.DomainModelLayer.Contracts.Repositories
{
    public interface IUserRepository : IBaseRepository<Users, Guid>
    {
    }
}

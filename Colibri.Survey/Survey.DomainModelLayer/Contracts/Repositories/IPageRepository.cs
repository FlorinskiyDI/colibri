﻿using System;
using dataaccesscore.Abstractions.Repositories;
using Survey.DomainModelLayer.Entities;

namespace Survey.DomainModelLayer.Contracts.Repositories
{
    public interface IPageRepository : IBaseRepository<Pages, Guid>
    {
    }
}

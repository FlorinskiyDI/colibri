using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Survey.DomainModelLayer.Entities;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface IOptionGroupService
    {
        Task<Guid> AddAsync();
    }
}

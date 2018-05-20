using Survey.ApplicationLayer.Dtos.Entities;
using Survey.DomainModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<Guid> AddAsync(Guid identityUserId);
        Task<UsersDto> GetAsync(Guid userId);
    }
}

using IdentityServer.Webapi.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Groups>> GetAllAsync();
        Task<IEnumerable<Groups>> GetSubGroupsAsync(Guid id);
        Task<Groups> GetAsync(Guid id);
    }
}

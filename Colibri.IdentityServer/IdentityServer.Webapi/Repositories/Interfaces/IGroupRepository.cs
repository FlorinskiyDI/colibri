using IdentityServer.Webapi.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Groups>> GetAllAsync(string id);
        Task<IEnumerable<Groups>> GetSubGroupsAsync(Guid? groupId, string userId);
        Task<Groups> GetAsync(Guid id);
        Task<Groups> CreateGroupAsync(Groups group);
        void DeleteGroup(Guid id);
    }
}

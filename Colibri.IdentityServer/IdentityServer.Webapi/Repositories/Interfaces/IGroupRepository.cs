using IdentityServer.Webapi.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Groups>> GetSubGroupsAsync(Guid? groupId);
        Task<IEnumerable<Groups>> GetRootAsync(string userId);
        Task<IEnumerable<Groups>> GetRootWithInverseAsync(string userId);
        Task<Groups> GetAsync(Guid id);
        Task<Groups> CreateGroupAsync(Groups group);
        void DeleteGroup(Guid id);
        Groups UpdateGroup(Groups group);
    }
}

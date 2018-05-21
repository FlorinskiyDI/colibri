using Survey.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.InfrastructureLayer.IdentityServices
{
    public interface IGroupRequestService
    {
        // group
        Task<IEnumerable<Groups>> GetGroupList();
        Task<IEnumerable<Groups>> GetGroupListRoot();
        Task<IEnumerable<Groups>> GetSubGroupList(Guid groupId);
        Task<Groups> CreateGroupAsync(Groups group);
        Task<Boolean> DeleteGroup(Guid groupId);
        Task<Groups> GetGroup(Guid groupId);
        Groups UpdateGroupt(Groups group);

        // group members
        Task<bool> AddMembersToGroupAsync(Guid groupId, List<string> emails);

    }
}

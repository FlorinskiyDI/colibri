using Survey.ApplicationLayer.Dtos.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface IGroupService
    {
        // group
        Task<IEnumerable<GroupDto>> GetGroupList();
        Task<IEnumerable<GroupDto>> GetGroupListRoot();
        Task<IEnumerable<GroupDto>> GetSubGroupList(Guid groupId);
        Task<GroupDto> CreateGroup(GroupDto groupDto);
        Task<bool> DeleteGroup(Guid groupId);
        Task<GroupDto> GetGroup(Guid groupId);
        GroupDto UpdateGroup(GroupDto groupDto);

        // group members
        Task<bool> AddMembersToGroupAsync(Guid groupId, List<string> emails);
    }
}

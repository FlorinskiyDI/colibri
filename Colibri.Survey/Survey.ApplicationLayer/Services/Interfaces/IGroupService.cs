using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Dtos.Models.IdentityServer.Pager;
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
        Task<PageDataDto<GroupDto>> GetGroupListRoot(PageSearchEntryDto searchEntryDto);
        Task<IEnumerable<GroupDto>> GetSubGroupList(Guid groupId);
        Task<GroupDto> CreateGroup(GroupDto groupDto);
        Task<bool> DeleteGroup(Guid groupId);
        Task<GroupDto> GetGroup(Guid groupId);
        GroupDto UpdateGroup(GroupDto groupDto);

        // group members
        Task<bool> AddMembersToGroupAsync(Guid groupId, List<string> emails);
        Task<bool> DeleteMemberFromGroupAsync(Guid groupId, string userId);
    }
}

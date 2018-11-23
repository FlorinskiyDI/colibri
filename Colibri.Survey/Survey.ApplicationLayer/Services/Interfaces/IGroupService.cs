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
        Task<PageDataDto<GroupDto>> GetGroups(PageSearchEntryDto searchEntryDto);
        Task<PageDataDto<GroupDto>> GetRootGroups(PageSearchEntryDto searchEntryDto);
        Task<IEnumerable<GroupDto>> GetSubgroups(SearchEntryDto searchEntryDto, string parentId);


        //Task<IEnumerable<GroupDto>> GetSubGroupList(Guid groupId);
        //Task<GroupDto> CreateGroup(GroupDto groupDto);
        //Task<bool> DeleteGroup(Guid groupId);
        //Task<GroupDto> GetGroup(Guid groupId);
        //GroupDto UpdateGroup(GroupDto groupDto);

        //// group members
        //Task<bool> AddMembersToGroupAsync(Guid groupId, List<string> emails);
        //Task<bool> DeleteMemberFromGroupAsync(Guid groupId, string userId);
    }
}

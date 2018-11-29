using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Dtos.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface IGroupService
    {
        Task<SearchResultDto<GroupDto>> GetGroups(SearchQueryDto searchEntryDto);
        Task<SearchResultDto<GroupDto>> GetRootGroups(SearchQueryDto searchEntryDto);
        Task<IEnumerable<GroupDto>> GetSubgroups(SearchQueryDto searchEntryDto, string parentId);


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

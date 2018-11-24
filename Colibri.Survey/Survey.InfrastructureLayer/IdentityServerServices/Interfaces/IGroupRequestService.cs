using Survey.DomainModelLayer.Models.IdentityServer;
using Survey.DomainModelLayer.Models.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survey.InfrastructureLayer.IdentityServices
{
    public interface IGroupRequestService
    {
        Task<SearchResultModel<GroupModel>> GetGroups(SearchQueryModel pageSearchEntry);
        Task<SearchResultModel<GroupModel>> GetRootGroups(SearchQueryModel pageSearchEntry);
        Task<IEnumerable<GroupModel>> GetSubgroups(SearchQueryModel searchEntry, string parentId);


        //// group
        //Task<IEnumerable<Groups>> GetGroupList();
        //Task<PageDataModel<Groups>> GetGroupListRoot(PageSearchEntryModel pageSearchEntry);
        //Task<IEnumerable<Groups>> GetSubGroupList(Guid groupId);
        //Task<Groups> CreateGroupAsync(Groups group);
        //Task<Boolean> DeleteGroup(Guid groupId);
        //Task<Groups> GetGroup(Guid groupId);
        //Groups UpdateGroupt(Groups group);

        //// group members
        //Task<bool> AddMembersToGroupAsync(Guid groupId, List<string> emails);
        //Task<bool> DeleteMemberFromGroupAsync(Guid groupId, string userId);
    }
}

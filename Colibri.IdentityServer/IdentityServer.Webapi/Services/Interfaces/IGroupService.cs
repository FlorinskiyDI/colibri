using dataaccesscore.EFCore.Paging;
using IdentityServer.Webapi.Dtos;
using IdentityServer.Webapi.Dtos.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services.Interfaces
{
    public interface IGroupService
    {

        Task<SearchResult<GroupDto>> GetGroupsAsync(string userId, SearchQuery searchEntry, bool isRoot = false);
        Task<IEnumerable<GroupDto>> GetByParentIdAsync(string userId, SearchQuery searchEntry, string parentId);


        //Task<DataPage<Groups, Guid>> GetRootAsync(PageSearchEntry searchEntry, string userId);
        //void SubscribeToGroupAsync(string userId, Guid groupId);
        //Task UnsubscribeToGroup(string userId, Guid groupId);
    }
}

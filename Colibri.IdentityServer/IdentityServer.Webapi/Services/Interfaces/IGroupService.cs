using dataaccesscore.EFCore.Paging;
using IdentityServer.Webapi.Dtos;
using IdentityServer.Webapi.Dtos.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services.Interfaces
{
    public interface IGroupService
    {
        Task<SearchResult<GroupDto>> GetRootAsync(string userId, SearchQuery searchEntry);
        Task<SearchResult<GroupDto>> GetAllAsync(string userId, SearchQuery searchEntry);
        Task<IEnumerable<GroupDto>> GetByParentIdAsync(string userId, SearchQuery searchEntry, string parentId);
        Task<GroupDto> CreateGroup(GroupDto model, string userId);
        Task DeleteGroup(string groupId, string userId);
        Task<GroupDto> UpdateGroup(GroupDto model, string userId);
        //Task<DataPage<Groups, Guid>> GetRootAsync(PageSearchEntry searchEntry, string userId);
        //void SubscribeToGroupAsync(string userId, Guid groupId);
        //Task UnsubscribeToGroup(string userId, Guid groupId);
    }
}

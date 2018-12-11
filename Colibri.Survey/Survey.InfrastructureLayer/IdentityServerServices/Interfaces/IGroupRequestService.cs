using Survey.DomainModelLayer.Models.IdentityServer;
using Survey.DomainModelLayer.Models.Search;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survey.InfrastructureLayer.IdentityServices
{
    public interface IGroupRequestService
    {
        Task<SearchResultModel<GroupModel>> GetGroups(SearchQueryModel pageSearchEntry);
        Task<SearchResultModel<GroupModel>> GetRootGroups(SearchQueryModel pageSearchEntry);
        Task<IEnumerable<GroupModel>> GetSubgroups(SearchQueryModel searchEntry, string parentId);
        Task<GroupModel> CreateGroupAsync(GroupModel model);
        Task DeleteGroupAsync(string model);
        Task<GroupModel> GetGroup(Guid groupId);
        Task<GroupModel> UpdateGroup(GroupModel model);
    }
}

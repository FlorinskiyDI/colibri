using IdentityServer.Webapi.Dtos;
using IdentityServer.Webapi.Dtos.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services.Interfaces
{
    public interface IMemberService
    {
        Task<SearchResult<MemberDto>> GetMembersByGroup(string groupId, SearchQuery searchEntry);

        //Task<bool> AddMembersToGroupAsync(Guid groupId, List<string> emailList);
        //Task<IEnumerable<ApplicationUser>> GetMembersForGroupAsync(Guid groupId);
        //Task DeleteMember(string userId, Guid groupId);
    }
}

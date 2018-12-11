using Survey.DomainModelLayer.Models.IdentityServer;
using Survey.DomainModelLayer.Models.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survey.InfrastructureLayer.IdentityServerServices.Interfaces
{
    public interface IGroupMemberRequestService
    {
        Task<SearchResultModel<MemberModel>> GetMembers(string groupId, SearchQueryModel pageSearchEntry);
        Task AddMembers(string groupId, IEnumerable<string> emailList);
        Task UnsubscribeMember(string groupId, string memberId);
    }
}

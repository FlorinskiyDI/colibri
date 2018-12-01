using RestSharp;
using Survey.DomainModelLayer.Models.IdentityServer;
using Survey.DomainModelLayer.Models.Search;
using Survey.InfrastructureLayer.IdentityServerServices.Interfaces;
using System.Threading.Tasks;

namespace Survey.InfrastructureLayer.IdentityServerServices
{
    public class GroupMemberRequestService : BaseIdentityServerService, IGroupMemberRequestService
    {

        public async Task<SearchResultModel<MemberModel>> GetMembers(string groupId, SearchQueryModel pageSearchEntry)
        {
            var restClient = await GetRestClient();
            var request = new RestRequest("/api/groups/" + groupId + "/members/search"  , Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(pageSearchEntry);
            IRestResponse<SearchResultModel<MemberModel>> response = await restClient.ExecuteTaskAsync<SearchResultModel<MemberModel>>(request);

            return response.IsSuccessful ? response.Data : null;
        }

    }
}

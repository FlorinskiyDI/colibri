using IdentityModel.Client;
using RestSharp;
using RestSharp.Authenticators;
using Survey.Common.Context;
using Survey.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.InfrastructureLayer.IdentityServices
{
    public class GroupRequestService : IGroupRequestService
    {
        public async Task<IEnumerable<Groups>> GetGroupList()
        {
            IEnumerable<Groups> list = new List<Groups>();
            var disco = await DiscoveryClient.GetAsync(NTContext.Context.IdentityUrl);
            var client = new TokenClient(disco.TokenEndpoint, "api1", "secret");
            var tokenResponse = await client.RequestCustomGrantAsync("delegation", "api2", new { token = NTContext.Context.IdentityUserToken });

            // call to identity server for get groups
            var restClient = new RestClient(NTContext.Context.IdentityUrl);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");
            var request = new RestRequest("/api/groups", Method.GET);
            IRestResponse<List<Groups>> response = await restClient.ExecuteTaskAsync<List<Groups>>(request);
            list = response.Data;

            if (!response.IsSuccessful)
            {
                return null;
            }
            else
            {
                return list;
            }
        }

        public async Task<IEnumerable<Groups>> GetSubGroupList(Guid groupId)
        {
            IEnumerable<Groups> list = new List<Groups>();
            var disco = await DiscoveryClient.GetAsync(NTContext.Context.IdentityUrl);
            var client = new TokenClient(disco.TokenEndpoint, "api1", "secret");
            var tokenResponse = await client.RequestCustomGrantAsync("delegation", "api2", new { token = NTContext.Context.IdentityUserToken });

            // call to identity server for get groups
            var restClient = new RestClient(NTContext.Context.IdentityUrl);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");
            var request = new RestRequest("/api/groups/" + groupId + "/subgroups", Method.GET);
            IRestResponse<List<Groups>> response = await restClient.ExecuteTaskAsync<List<Groups>>(request);
            list = response.Data;

            if (!response.IsSuccessful)
            {
                return null;
            }
            else
            {
                return list;
            }
        }
    }
}

using IdentityModel.Client;
using RestSharp;
using RestSharp.Authenticators;
using Survey.Common.Context;
using Survey.DomainModelLayer.Models;
using Survey.InfrastructureLayer.IdentityServerServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.InfrastructureLayer.IdentityServices
{
    public class GroupRequestService: BaseApiService,  IGroupRequestService
    {
        public async Task<Groups> CreateGroupAsync(Groups group)
        {            
            var tokenResponse = await GetToken();

            // call to identity server for create groups
            var restClient = new RestClient(NTContext.Context.IdentityUrl);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");
            var request = new RestRequest("/api/groups", Method.POST) {
                RequestFormat = DataFormat.Json
            };
            request.AddBody(group);
            IRestResponse<Groups> response = await restClient.ExecuteTaskAsync<Groups>(request);

            return response.IsSuccessful ? response.Data : null;
        }

        public Groups UpdateGroupt(Groups group)
        {
            var tokenResponse = GetToken().Result;
            // call to identity server for update group
            var restClient = new RestClient(NTContext.Context.IdentityUrl);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");
            var request = new RestRequest("/api/groups", Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddBody(group);
            IRestResponse<Groups> response = restClient.Execute<Groups>(request);
            //
            return response.IsSuccessful ? response.Data : null;
        }

        public async Task<IEnumerable<Groups>> GetGroupListRoot()
        {
            IEnumerable<Groups> list = new List<Groups>();
            var tokenResponse = await GetToken();
            // call to identity server for get groups
            var restClient = new RestClient(NTContext.Context.IdentityUrl);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");
            var request = new RestRequest("/api/groups/root", Method.GET);
            IRestResponse<List<Groups>> response = await restClient.ExecuteTaskAsync<List<Groups>>(request);
            //
            return response.IsSuccessful ? response.Data : null;
        }

        public async Task<IEnumerable<Groups>> GetGroupList()
        {
            IEnumerable<Groups> list = new List<Groups>();
            var tokenResponse = await GetToken();
            // call to identity server for get groups
            var restClient = new RestClient(NTContext.Context.IdentityUrl);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");
            var request = new RestRequest("/api/groups", Method.GET);
            IRestResponse<List<Groups>> response = await restClient.ExecuteTaskAsync<List<Groups>>(request);
            //
            return response.IsSuccessful ? response.Data : null;
        }

        public async Task<Boolean> DeleteGroup(Guid groupId)
        {
            var tokenResponse = await GetToken();

            // call to identity server for delete group
            var restClient = new RestClient(NTContext.Context.IdentityUrl);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");
            var request = new RestRequest("/api/groups/"+ groupId, Method.DELETE);
            IRestResponse response = restClient.Execute(request);

            if (!response.IsSuccessful)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<Groups> GetGroup(Guid groupId)
        {
            var tokenResponse = await GetToken();

            // call to identity server for get group
            var restClient = new RestClient(NTContext.Context.IdentityUrl);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");
            var request = new RestRequest("/api/groups/" + groupId, Method.GET);
            IRestResponse<Groups> response = await restClient.ExecuteTaskAsync<Groups>(request);

            if (!response.IsSuccessful)
            {
                return null;
            }
            else
            {
                return response.Data;
            }
        }

        public async Task<IEnumerable<Groups>> GetSubGroupList(Guid groupId)
        {
            IEnumerable<Groups> list = new List<Groups>();
            var tokenResponse = await GetToken();

            // call to identity server for get sub groups
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

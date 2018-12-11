using RestSharp;
using Survey.DomainModelLayer.Models.IdentityServer;
using Survey.DomainModelLayer.Models.Search;
using Survey.InfrastructureLayer.IdentityServerServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survey.InfrastructureLayer.IdentityServices
{
    public class GroupRequestService : BaseIdentityServerService, IGroupRequestService
    {

        public async Task<SearchResultModel<GroupModel>> GetGroups(SearchQueryModel pageSearchEntry)
        {
            var restClient = await GetRestClient();
            var request = new RestRequest("/api/groups/search", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(pageSearchEntry);
            IRestResponse<SearchResultModel<GroupModel>> response = await restClient.ExecuteTaskAsync<SearchResultModel<GroupModel>>(request);

            return response.IsSuccessful ? response.Data : null;
        }

        public async Task<SearchResultModel<GroupModel>> GetRootGroups(SearchQueryModel pageSearchEntry)
        {
            var restClient = await GetRestClient();
            var request = new RestRequest("/api/groups/root", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(pageSearchEntry);
            IRestResponse<SearchResultModel<GroupModel>> response = await restClient.ExecuteTaskAsync<SearchResultModel<GroupModel>>(request);

            return response.IsSuccessful ? response.Data : null;
        }

        public async Task<IEnumerable<GroupModel>> GetSubgroups(SearchQueryModel searchEntry, string parentId)
        {
            var restClient = await GetRestClient();
            var request = new RestRequest("/api/groups/" + parentId + "/subgroups", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(searchEntry);
            IRestResponse<IEnumerable<GroupModel>> response = await restClient.ExecuteTaskAsync<IEnumerable<GroupModel>>(request);

            return response.IsSuccessful ? response.Data : null;
        }

        public async Task<GroupModel> CreateGroupAsync(GroupModel model)
        {
            var restClient = await GetRestClient();
            var request = new RestRequest("/api/groups", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(model);
            IRestResponse<GroupModel> response = await restClient.ExecuteTaskAsync<GroupModel>(request);

            return response.IsSuccessful ? response.Data : null;
        }

        public async Task DeleteGroupAsync(string groupId)
        {
            var restClient = await GetRestClient();
            var request = new RestRequest("/api/groups/" + groupId , Method.DELETE) { RequestFormat = DataFormat.Json };
            IRestResponse response = await restClient.ExecuteTaskAsync(request);

            return;
        }

        public async Task<GroupModel> GetGroup(Guid groupId)
        { 
            var restClient = await GetRestClient();
            var request = new RestRequest("/api/groups/" + groupId, Method.GET);
            IRestResponse<GroupModel> response = await restClient.ExecuteTaskAsync<GroupModel>(request);

            if (response.ErrorException == null)
            {
                return response.Data;
            }
            else
            {
                var ex = new ApplicationException("Error retrieving response.  Check inner details for more info.", response.ErrorException);
                throw ex;
            }
        }

        public async Task<GroupModel> UpdateGroup(GroupModel model)
        {
            var restClient = await GetRestClient();
            var request = new RestRequest("/api/groups", Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddBody(model);
            IRestResponse<GroupModel> response = await restClient.ExecuteTaskAsync<GroupModel>(request);

            if (response.ErrorException == null)
            {
                return response.Data;
            }
            else
            {
                throw new ApplicationException("Error retrieving response.  Check inner details for more info.", response.ErrorException);
            }
        }

        //public async Task<Groups> CreateGroupAsync(Groups group)
        //{            
        //    var tokenResponse = await GetToken();

        //    // call to identity server for create groups
        //    var restClient = new RestClient(NTContext.Context.IdentityUrl);
        //    restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");

        //    var request = new RestRequest("/api/groups", Method.POST) { RequestFormat = DataFormat.Json };
        //    request.AddBody(group);
        //    IRestResponse<Groups> response = await restClient.ExecuteTaskAsync<Groups>(request);

        //    return response.IsSuccessful ? response.Data : null;
        //}

        //public Groups UpdateGroupt(Groups group)
        //{
        //    var tokenResponse = GetToken().Result;
        //    // call to identity server for update group
        //    var restClient = new RestClient(NTContext.Context.IdentityUrl);
        //    restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");
        //    var request = new RestRequest("/api/groups", Method.PUT) { RequestFormat = DataFormat.Json };
        //    request.AddBody(group);
        //    IRestResponse<Groups> response = restClient.Execute<Groups>(request);
        //    //
        //    return response.IsSuccessful ? response.Data : null;
        //}

        //public async Task<PageDataModel<Groups>> GetGroupListRoot(PageSearchEntryModel pageSearchEntry)
        //{
        //    IEnumerable<Groups> list = new List<Groups>();
        //    var tokenResponse = await GetToken();
        //    // call to identity server for get groups
        //    var restClient = new RestClient(NTContext.Context.IdentityUrl);
        //    restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");

        //    var request = new RestRequest("/api/groups/root", Method.POST) { RequestFormat = DataFormat.Json };
        //    request.AddBody(pageSearchEntry);
        //    IRestResponse<PageDataModel<Groups>> response = await restClient.ExecuteTaskAsync<PageDataModel<Groups>>(request);
        //    //
        //    return response.IsSuccessful ? response.Data : null;
        //}

        //public async Task<IEnumerable<Groups>> GetGroupList()
        //{
        //    IEnumerable<Groups> list = new List<Groups>();
        //    var tokenResponse = await GetToken();
        //    // call to identity server for get groups
        //    var restClient = new RestClient(NTContext.Context.IdentityUrl);
        //    restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");
        //    var request = new RestRequest("/api/groups", Method.GET);
        //    IRestResponse<List<Groups>> response = await restClient.ExecuteTaskAsync<List<Groups>>(request);
        //    //
        //    return response.IsSuccessful ? response.Data : null;
        //}

        //public async Task<Boolean> DeleteGroup(Guid groupId)
        //{
        //    var tokenResponse = await GetToken();

        //    // call to identity server for delete group
        //    var restClient = new RestClient(NTContext.Context.IdentityUrl);
        //    restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");
        //    var request = new RestRequest("/api/groups/"+ groupId, Method.DELETE);
        //    IRestResponse response = restClient.Execute(request);

        //    if (!response.IsSuccessful)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}



        //public async Task<IEnumerable<Groups>> GetSubGroupList(Guid groupId)
        //{
        //    IEnumerable<Groups> list = new List<Groups>();
        //    var tokenResponse = await GetToken();

        //    // call to identity server for get sub groups
        //    var restClient = new RestClient(NTContext.Context.IdentityUrl);
        //    restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");
        //    var request = new RestRequest("/api/groups/" + groupId + "/subgroups", Method.GET);
        //    IRestResponse<List<Groups>> response = await restClient.ExecuteTaskAsync<List<Groups>>(request);
        //    list = response.Data;

        //    if (!response.IsSuccessful)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return list;
        //    }
        //}

        //#region group members

        //public async Task<bool> AddMembersToGroupAsync(Guid groupId, List<string> emails)
        //{
        //    var tokenResponse = await GetToken();

        //    // call to identity server for create groups
        //    var restClient = new RestClient(NTContext.Context.IdentityUrl);
        //    restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");
        //    var request = new RestRequest("/api/groups/" + groupId + "/members" , Method.POST)
        //    {
        //        RequestFormat = DataFormat.Json
        //    };
        //    request.AddBody(emails);
        //    IRestResponse<bool> response = await restClient.ExecuteTaskAsync<bool>(request);
        //    //
        //    return await Task.FromResult(response.IsSuccessful);
        //}

        //public async Task<bool> DeleteMemberFromGroupAsync(Guid groupId, string userId)
        //{
        //    var tokenResponse = await GetToken();
        //    // call to identity server for create groups
        //    var restClient = new RestClient(NTContext.Context.IdentityUrl);
        //    restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");
        //    var request = new RestRequest($"/api/groups/{groupId}/members/{userId}", Method.DELETE);
        //    IRestResponse<bool> response = await restClient.ExecuteTaskAsync<bool>(request);
        //    //
        //    return await Task.FromResult(response.IsSuccessful);
        //}

        //#endregion

    }
}

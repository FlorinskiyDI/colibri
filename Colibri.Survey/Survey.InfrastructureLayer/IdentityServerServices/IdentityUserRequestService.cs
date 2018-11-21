using RestSharp;
using RestSharp.Authenticators;
using Survey.Common.Context;
using Survey.DomainModelLayer.Models.IdentityServer;
using Survey.InfrastructureLayer.IdentityServerServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survey.InfrastructureLayer.IdentityServerServices
{
    public class IdentityUserRequestService : BaseIdentityServerService,  IIdentityUserRequestService
    {
        public async Task<IEnumerable<IdentityUserModel>> GetIdentityUsersAsync(Guid groupId)
        {
            try
            {
                var tokenResponse = await GetToken();
                //
                var restClient = new RestClient(NTContext.Context.IdentityUrl);
                restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");
                var request = new RestRequest("/api/groups/" + groupId + "/members", Method.GET);
                IRestResponse<IEnumerable<IdentityUserModel>> response = await restClient.ExecuteTaskAsync<IEnumerable<IdentityUserModel>>(request);
                //
                return response.IsSuccessful ? response.Data : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            
        }
    }
}

using IdentityModel.Client;
using Survey.Common.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.InfrastructureLayer.IdentityServerServices
{
    public abstract class BaseApiService
    {
        protected async Task<TokenResponse> GetToken()
        {
            var disco = await DiscoveryClient.GetAsync(NTContext.Context.IdentityUrl);
            var client = new TokenClient(disco.TokenEndpoint, "api1", "secret");
            var tokenResponse = await client.RequestCustomGrantAsync("delegation", "api2", new { token = NTContext.Context.IdentityUserToken });

            return tokenResponse;
        }
    }
}

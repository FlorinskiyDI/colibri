using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using Survey.Webapi.Models.RestModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Survey.Webapi.Controllers
{
    [Authorize()]
    [Route("api/groups")]
    public class GroupsController : Controller
    {
        private IConfiguration _configuration;

        public GroupsController(
            IConfiguration configuration
        )
        {
            _configuration = configuration;
        }


        // GET: api/groups
        [HttpGet]
        public async Task<IActionResult> GetListGroups()
        {
            var identityServerApiUrl = _configuration["IdentityServer:ApiUrl"];  // IdentityServer4 host
            var disco = await DiscoveryClient.GetAsync(identityServerApiUrl); // discover endpoints from metadata
            var userToken = await HttpContext.GetTokenAsync("access_token"); // user token
            var idToken = await HttpContext.GetTokenAsync("id_token"); // user token

            // create token client
            var client = new TokenClient(disco.TokenEndpoint, "api1", "secret");
            // send custom grant to token endpoint, return response
            var payload = new { token = userToken };
            var tokenResponse = await client.RequestCustomGrantAsync("delegation", "api2", payload);

            // call to identity server for get groups
            var restClient = new RestClient("http://localhost:5050");
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(tokenResponse.AccessToken, "Bearer");
            var request = new RestRequest();
            request.Resource = "/api/groups";
            request.Method = Method.GET;
            IRestResponse<List<Groups>> response = await restClient.ExecuteTaskAsync<List<Groups>>(request);

            if (!response.IsSuccessful)
            {
                Console.WriteLine(response.StatusCode);
                return StatusCode(500);
            }
            else
            {
                return Ok(response.Data);
            }
        }

    }

    public class Test
    {
        public string Value { get; set; }
    }
}

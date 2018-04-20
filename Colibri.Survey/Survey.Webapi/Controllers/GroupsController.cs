using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Survey.Webapi.Controllers
{
    [Authorize()]
    [Route("api/[controller]")]
    public class GroupsController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var identityServerApiUrl = "http://localhost:5050";  // IdentityServer4 host
            var disco = await DiscoveryClient.GetAsync(identityServerApiUrl); // discover endpoints from metadata
            var userToken = await HttpContext.GetTokenAsync("access_token"); // user token

            // create token client
            var client = new TokenClient(disco.TokenEndpoint, "api1", "secret");
            // send custom grant to token endpoint, return response
            var payload = new { token = userToken };
            var tokenResponse = await client.RequestCustomGrantAsync("delegation", "api2", payload);

            //var restClient = new RestClient(identityServerApiUrl);
            //var request = new RestRequest("/api/groups", Method.GET);
            //restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(userToken);
            //var response = await restClient.ExecuteTaskAsync(request);

            //call api
            var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            var response = await httpClient.GetAsync("http://localhost:5050/api/groups");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("User claims \n" + JArray.Parse(content));
            }

            var list = new List<Test>();
            list.Add(new Test());
            list.Add(new Test());
            list.Add(new Test());

            return Ok(list);
        }

    }

    public class Test
    {
        public string Value { get; set; }
    }
}

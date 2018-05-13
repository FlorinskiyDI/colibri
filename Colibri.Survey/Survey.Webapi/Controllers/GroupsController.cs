using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.Webapi.Models.RestModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Survey.Webapi.Controllers
{
    [Authorize()]
    [Route("api/groups")]
    public class GroupsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IGroupService _groupService;

        public GroupsController(
            IConfiguration configuration,
            IGroupService groupService
        )
        {
            _configuration = configuration;
            _groupService = groupService;
        }


        // GET: api/groups
        [HttpGet]
        public async Task<IActionResult> GetListGroups()
        {

            var result = await _groupService.GetGroupList();
            return Ok(result);
        }
    }

    public class Test
    {
        public string Value { get; set; }
    }
}

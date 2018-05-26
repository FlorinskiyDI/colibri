using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IdentityServer.Webapi.Repositories.Interfaces;
using IdentityServer.Webapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServer.Webapi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "user")]
    [Produces("application/json")]
    [Route("api/groups/{groupId?}/members")]
    public class GroupsMembersController : Controller
    {        
        private readonly IAppUserRepository _appUserRepository;
        private readonly IIdentityUserService _identityUserService;
        public GroupsMembersController(
            IAppUserRepository appUserRepository,
            IIdentityUserService identityUserService
        )
        {
            _appUserRepository = appUserRepository;
            _identityUserService = identityUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers(Guid groupId)
        {
            var list = await _appUserRepository.GetAppUsersForGroup(groupId);
            return Ok(list);
            
        }

        [HttpPost]
        public async Task<IActionResult> AddMembers(Guid groupId, [FromBody] List<string> emails)
        {
            var list = await _identityUserService.AddMembersFroupAsync(groupId, emails);
            return Ok();

        }
    }
}

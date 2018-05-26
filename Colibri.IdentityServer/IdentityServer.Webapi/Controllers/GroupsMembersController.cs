using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer.Webapi.Repositories.Interfaces;
using IdentityServer.Webapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Webapi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "user")]
    [Produces("application/json")]
    [Route("api/groups/members")]
    [Route("api/groups/{groupId?}/members")]
    public class GroupsMembersController : Controller
    {
        // GET: api/groups/{groupId}/members
        // POST: api/groups/{groupId}/members

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

        // GET: api/groups/{groupId}/members
        [HttpGet]
        public async Task<IActionResult> GetMembers(Guid groupId)
        {
            var list = await _appUserRepository.GetAppUsersForGroup(groupId);
            return Ok(list);
        }

        // POST: api/groups/{groupId}/members
        [HttpPost]
        public async Task<IActionResult> AddMembers(Guid groupId, [FromBody] List<string> emails)
        {
            var list = await _identityUserService.AddMembersFroupAsync(groupId, emails);
            return Ok();

        }
    }
}

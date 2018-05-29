using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer.Webapi.Repositories.Interfaces;
using IdentityServer.Webapi.Services;
using IdentityServer.Webapi.Services.Interfaces;
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
        // DELETE: api/groups/{groupId}/members/{id}

        private readonly IGroupMemberService _groupMemberService;
        public GroupsMembersController(
            IGroupMemberService groupMemberService
        )
        {
            _groupMemberService = groupMemberService;
        }

        // GET: api/groups/{groupId}/members
        [HttpGet]
        public async Task<IActionResult> GetMembers(Guid groupId)
        {
            var list = await _groupMemberService.GetMembersForGroupAsync(groupId);
            return Ok(list);
        }

        // POST: api/groups/{groupId}/members
        [HttpPost]
        public async Task<IActionResult> AddMembers(Guid groupId, [FromBody] List<string> emails)
        {
            var list = await _groupMemberService.AddMembersToGroupAsync(groupId, emails);
            return Ok();
        }

        // DELETE: api/groups/{groupId}/members/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(Guid groupId, string id)
        {
            await _groupMemberService.DeleteMember(id, groupId);
            return Ok();
        }

    }
}

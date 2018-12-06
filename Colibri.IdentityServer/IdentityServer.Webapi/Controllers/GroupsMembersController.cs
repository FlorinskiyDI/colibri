using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Webapi.Dtos.Search;
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
        // POST: api/groups/search
        // POST: api/groups/{groupId}/members
        // DELETE: api/groups/{groupId}/members/{id}

        private readonly IMemberService _groupMemberService;
        public GroupsMembersController(
            IMemberService groupMemberService
        )
        {
            _groupMemberService = groupMemberService;
        }


        // POST: api/groups/1/members/search
        [HttpPost("search")]
        public async Task<IActionResult> GetMembers([FromBody] SearchQuery searchEntry, [FromRoute] string groupId)
        {
            var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            var result = await _groupMemberService.GetMembersByGroup(groupId, searchEntry);
            return Ok(result);
        }



        //// GET: api/groups/{groupId}/members
        //[HttpGet]
        //public async Task<IActionResult> GetMembers(Guid groupId)
        //{
        //    var list = await _groupMemberService.GetMembersForGroupAsync(groupId);
        //    return Ok(list);
        //}

        //// POST: api/groups/{groupId}/members
        //[HttpPost]
        //public async Task<IActionResult> AddMembers(Guid groupId, [FromBody] List<string> emails)
        //{
        //    var list = await _groupMemberService.AddMembersToGroupAsync(groupId, emails);
        //    return Ok();
        //}

        //// DELETE: api/groups/{groupId}/members/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMember(Guid groupId, string id)
        //{
        //    await _groupMemberService.DeleteMember(id, groupId);
        //    return Ok();
        //}

    }
}

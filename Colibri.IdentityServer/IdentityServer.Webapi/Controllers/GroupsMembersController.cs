﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Webapi.Configurations;
using IdentityServer.Webapi.Dtos;
using IdentityServer.Webapi.Dtos.Search;
using IdentityServer.Webapi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Webapi.Controllers
{
    [Authorize]
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

        // POST: api/groups/1/members
        [HttpPost]
        public async Task<IActionResult> AddMembers([FromBody] IEnumerable<string> emailList, [FromRoute] Guid groupId)
        {
            var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            await _groupMemberService.AddMembersToGroupByEmailsAsync(emailList, groupId);
            return Ok();
        }

        // DELETE: api/groups/members/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember([FromRoute] Guid groupId, [FromRoute] Guid id)
        {
            var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            await _groupMemberService.DeleteMemberOfGroupAsync(id);
            return Ok();
        }


        // GET: api/users/getIamPolicy
        [HttpGet("iamPolicy")]
        [Authorize(Policy = SystemStaticPermissions.Groups.GetIamPolicy)]
        public async Task<IActionResult> GetIamPolicy()
        {
            var result = new List<string> { "Admin", "GroupCreator" }; //TODO: get role (db)
            return Ok(result);
        }


        // POST: api/users/iamPolicy
        [HttpPost("iamPolicy")]
        //[Authorize(Policy = SystemStaticPermissions.Groups.SetIamPolicy)]
        public async Task<IActionResult> SetIamPolicy([FromBody] GroupPolicyDto model)
        {
            //var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            var groupId = model.GroupId;
            await _groupMemberService.SetPolicy(model, groupId);
            return Ok();
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

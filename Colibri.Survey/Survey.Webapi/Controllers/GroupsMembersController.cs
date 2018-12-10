using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Survey.ApplicationLayer.Dtos.Search;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.Webapi.Controllers.Base;
using Survey.Webapi.Services.TypeHelper;

namespace Survey.Webapi.Controllers
{
    [Authorize()]
    [Route("api/groups/members")]
    [Route("api/groups/{groupId?}/members")]
    public class GroupsMembersController : BaseController
    {
        // POST: api/groups//{groupId?}/members/search

        private readonly IMemberService _memberService;
        public GroupsMembersController(
            IConfiguration configuration,
            ITypeHelperService typeHelperService,
            //
            IMemberService memberService
        ) : base(configuration, typeHelperService)
        {
            _memberService = memberService;
        }


        // POST: api/groups/1/members/search
        [HttpPost("search")]
        public async Task<IActionResult> GetMembers([FromBody] SearchQueryDto searchEntry, [FromRoute] string groupId)
        {
            var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            var result = await _memberService.GetMembersAsync(groupId, searchEntry);
            return Ok(result);
        }

        // POST: api/groups/1/members
        [HttpPost]
        public async Task<IActionResult> AddMembers([FromBody] IEnumerable<string> emailList, [FromRoute] string groupId)
        {
            var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            await _memberService.AddMembersAsync(groupId, emailList);
            return Ok();
        }
        //// GET: api/groups//{groupId?}/members
        //[HttpGet]
        //public async Task<IActionResult> GetMembers(Guid groupId)
        //{
        //    IEnumerable<IdentityUserDto> result;
        //    try
        //    {
        //        result = await _identityUserService.GetIdentityUsersForGroupAsync(groupId);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }

        //    return Ok(result);
        //}

        //// POST: api/groups//{groupId?}/members
        //[HttpPost]
        //public async Task<IActionResult> AddMembers(Guid groupId, [FromBody] List<string> emails)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        //var result = await _groupService.AddMembersToGroupAsync(groupId, emails);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        //// DELETE: api/groups/{groupId}/members/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMember(Guid groupId, string id)
        //{
        //    //await _groupService.DeleteMemberFromGroupAsync(groupId, id);
        //    return Ok();
        //}

    }
}

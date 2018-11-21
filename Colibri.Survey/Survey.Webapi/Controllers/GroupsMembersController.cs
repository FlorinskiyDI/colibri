using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.Webapi.Controllers.Base;
using Survey.Webapi.Helpers.Shaping;
using Survey.Webapi.Services.TypeHelper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Survey.Webapi.Controllers
{
    [Authorize()]
    [Route("api/groups/members")]
    [Route("api/groups/{groupId?}/members")]
    public class GroupsMembersController : BaseController
    {
        // GET: api/groups//{groupId?}/members
        // POST: api/groups//{groupId?}/members

        private readonly IIdentityUserService _identityUserService;
        private readonly IGroupService _groupService;
        public GroupsMembersController(
            IConfiguration configuration,
            ITypeHelperService typeHelperService,
            //
            IIdentityUserService identityUserService,
            IGroupService groupService
        ) : base(configuration, typeHelperService)
        {
            _identityUserService = identityUserService;
            _groupService = groupService;
        }

        // GET: api/groups//{groupId?}/members
        [HttpGet]
        public async Task<IActionResult> GetMembers(Guid groupId)
        {
            IEnumerable<IdentityUserDto> result;
            try
            {
                result = await _identityUserService.GetIdentityUsersForGroupAsync(groupId);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(result);
        }

        // POST: api/groups//{groupId?}/members
        [HttpPost]
        public async Task<IActionResult> AddMembers(Guid groupId, [FromBody] List<string> emails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                //var result = await _groupService.AddMembersToGroupAsync(groupId, emails);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/groups/{groupId}/members/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(Guid groupId, string id)
        {
            //await _groupService.DeleteMemberFromGroupAsync(groupId, id);
            return Ok();
        }

    }
}

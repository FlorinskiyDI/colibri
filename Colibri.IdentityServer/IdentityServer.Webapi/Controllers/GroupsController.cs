using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Webapi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "admin")]
    [Produces("application/json")]
    [Route("api/groups")]
    public class GroupsController : Controller
    {
        // repositories
        private readonly IGroupRepository _groupRepository;
        private readonly IAppUserGroupRepository _appUserGroupRepository;

        public GroupsController(
            IGroupRepository groupRepository,
            IAppUserGroupRepository appUserGroupRepository
        )
        {
            _groupRepository = groupRepository;
            _appUserGroupRepository = appUserGroupRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            var claims = this.HttpContext.User.Claims;
            var userId = claims.First(c => c.Type == "sub").Value;
            var list = await _groupRepository.GetAllAsync(userId);
            return Ok(list);
        }

        [HttpGet]
        [Route("{id}/subgroups")]
        public async Task<IActionResult> GetSubGroups(Guid id)
        {
            var claims = this.HttpContext.User.Claims;
            var userId = claims.First(c => c.Type == "sub").Value;
            var list = await _groupRepository.GetSubGroupsAsync(id, userId);
            return Ok(list);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGroup(Guid id)
        {
            Groups entity;
            try
            {
                entity  = await _groupRepository.GetAsync(id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(entity);
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteGroup(Guid id)
        {
            try
            {
                _appUserGroupRepository.DeleteAppUserGroupByGroupAsync(id);
                _groupRepository.DeleteGroup(id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }

        // POST: api/groups/
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] Groups model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var claims = this.HttpContext.User.Claims;
            var userId = claims.First(c => c.Type == "sub").Value;
            Groups group;
            try
            {
                group = await _groupRepository.CreateGroupAsync(model);
                await _appUserGroupRepository.CreateAppUserGroupAsync(new ApplicationUserGroups()
                {
                    GroupId = group.Id,
                    UserId = userId
                });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(group);
        }
    }
}
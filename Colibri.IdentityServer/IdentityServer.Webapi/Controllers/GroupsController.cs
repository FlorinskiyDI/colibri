using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Webapi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        public GroupsController(
            IGroupRepository groupRepository
        )
        {
            _groupRepository = groupRepository;
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
            var entity = await _groupRepository.GetAsync(id);
            return Ok(entity);
        }
    }

    public class MyTest
    {
        public string Value { get; set; }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using dataaccesscore.EFCore.Paging;
using IdentityServer.Webapi.Configurations;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Dtos;
using IdentityServer.Webapi.Dtos.Search;
using IdentityServer.Webapi.Repositories.Interfaces;
using IdentityServer.Webapi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Webapi.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/groups")]
    public class GroupsController : Controller
    {

        // POST: api/groups/search
        // POST: api/groups/root
        // POST: api/groups/{id}/subgroups

        // POST: api/groups
        // PUT: api/groups
        // DELETE: api/groups/{id}

        private readonly IDataPager<Groups> _pager;
        protected readonly IGroupService _groupServices;
        private readonly IGroupRepository _groupRepository;

        public GroupsController(
            IGroupService groupServices,
            IGroupRepository groupRepository,
            IDataPager<Groups> pager
        )
        {
            _groupRepository = groupRepository;
            _groupRepository = groupRepository;
            _groupServices = groupServices;
            _pager = pager;
        }

        // POST: api/groups/search
        [HttpPost("search")]
        [Authorize(Policy = SystemStaticPermissions.Groups.List)]
        public async Task<IActionResult> GetGroups([FromBody] SearchQuery searchEntry)
        {
            var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            var result = await _groupServices.GetAllAsync(_userId, searchEntry);
            return Ok(result);
        }


        // POST: api/groups/root
        [HttpPost("root")]
        [Authorize(Policy = SystemStaticPermissions.Groups.ListRoot)]
        public async Task<IActionResult> GetRootGroups([FromBody] SearchQuery searchEntry)
        {
            var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            var result = await _groupServices.GetRootAsync(_userId, searchEntry);
            return Ok(result);
        }

        // POST: api/groups/{id}/subgroups
        [HttpPost("{id}/subgroups")]
        [Authorize(Policy = SystemStaticPermissions.Groups.GetSubgroups)]
        public async Task<IActionResult> GetSubGroups([FromBody] SearchQuery searchEntry, string id)
        {
            var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            var result = await _groupServices.GetByParentIdAsync(_userId, searchEntry, id);
            return Ok(result);
        }

        // POST: api/groups
        [HttpPost]
        [Authorize(Policy = SystemStaticPermissions.Groups.Create)]
        public async Task<IActionResult> CreateGroup([FromBody] GroupDto model)
        {
            var userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            var result = await _groupServices.CreateGroup(model, userId);
            return Ok(result);
        }

        // DELETE: api/groups/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = SystemStaticPermissions.Groups.Delete)]
        public async Task<IActionResult> DeleteGroup(string id)
        {
            var userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            await _groupServices.DeleteGroup(id, userId);
            return Ok();
        }

        // PUT: api/groups/{id}/subgroups
        [HttpPut()]
        [Authorize(Policy = SystemStaticPermissions.Groups.Update)]
        public async Task<IActionResult> UpdateGroups([FromBody] GroupDto model)
        {
            var userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            var result = await _groupServices.UpdateGroup(model, userId);
            return Ok(result);
        }

        // GET: api/groups/{id}
        [HttpGet("{id}")]
        [Authorize(Policy = SystemStaticPermissions.Groups.Get)]
        public async Task<IActionResult> GetGroup(Guid id)
        {
            Groups entity;
            try
            {
                entity = await _groupRepository.GetAsync(id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(entity);
        }
    }

}
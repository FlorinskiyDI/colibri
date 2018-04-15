using System;
using System.Threading.Tasks;
using IdentityServer.Webapi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Webapi.Controllers
{
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
            var list = await _groupRepository.GetAllAsync();
            return Ok(list);
        }

        [HttpGet]
        [Route("{id}/subgroups")]
        public async Task<IActionResult> GetSubGroups(Guid id)
        {
            var list = await _groupRepository.GetSubGroupsAsync(id);
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
}
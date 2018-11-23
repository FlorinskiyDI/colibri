using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Dtos.Models.IdentityServer.Pager;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.Webapi.Controllers.Base;
using Survey.Webapi.Helpers.Shaping;
using Survey.Webapi.Services.TypeHelper;

namespace Survey.Webapi.Controllers
{
    [Authorize()]
    [Route("api/groups")]
    public class GroupsController : BaseController
    {
        // POST: api/groups
        // POST: api/groups/root
        // POST: api/groups/{id}/subgroups


        // GET: api/groups/{id}
        // POST: api/groups
        // PUT: api/groups
        // DELETE: api/groups/{id}

        private readonly IGroupService _groupService;
        public GroupsController(
            IConfiguration configuration,
            ITypeHelperService typeHelperService,
            IGroupService groupService
        ) : base(configuration, typeHelperService)
        {
            _groupService = groupService;
        }

        // POST: api/groups
        [HttpPost]
        public async Task<IActionResult> GetGroups([FromBody] PageSearchEntryDto searchEntry)
        {
            var result = await _groupService.GetGroups(searchEntry);
            return Ok(result);
        }

        // POST: api/groups/root
        [HttpPost("root")]
        public async Task<IActionResult> GetRootGroups([FromBody] PageSearchEntryDto searchEntry)
        {
            var result = await _groupService.GetRootGroups(searchEntry);
            return Ok(result);
        }

        // POST: api/groups/{id}/subgroups
        [Route("{id}/subgroups")]
        public async Task<IActionResult> GetSubgroupList([FromBody] SearchEntryDto searchEntry, string id)
        {
            var result = await _groupService.GetSubgroups(searchEntry, id);
            return Ok(result);
        }

        //// POST: api/groups
        //[HttpPost]
        //public async Task<IActionResult> GetGroups([FromQuery] string fields)
        //{
        //    if (!_typeHelperService.TypeHasProperties<GroupDto>(fields))
        //    {
        //        return BadRequest();
        //    }

        //    IEnumerable<GroupDto> result;
        //    try
        //    {
        //        result = await _groupService.GetGroupList();
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }

        //    return Ok(result.ShapeData(fields));
        //}

        //// POST: api/groups/root
        //[HttpPost("root")]
        //public async Task<IActionResult> GetGroupListRoot([FromBody] PageSearchEntryDto searchEntry, [FromQuery] string fields)
        //{
        //    if (!_typeHelperService.TypeHasProperties<GroupDto>(fields))
        //    {
        //        return BadRequest();
        //    }

        //    PageDataDto<GroupDto> result;
        //    try
        //    {
        //        result = await _groupService.GetGroupListRoot(searchEntry);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //    //var test = result.ShapeData(fields);
        //    return Ok(result);
        //}

        //// GET: api/groups/{id}/subgroups
        //[HttpGet]
        //[Route("{id}/subgroups")]
        //public async Task<IActionResult> GetSubGroupList(Guid id)
        //{
        //    IEnumerable<GroupDto> result;
        //    try
        //    {
        //        result = await _groupService.GetSubGroupList(id);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }

        //    return Ok(result);
        //}

        //// DELETE: api/groups/{id}
        //[HttpDelete]
        //[Route("{id}")]
        //public async Task<IActionResult> DeleteGroup(Guid id)
        //{
        //    try
        //    {
        //        var result = await _groupService.DeleteGroup(id);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }

        //    return Ok();
        //}

        //// GET: api/groups/{id}
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetGroup(Guid id)
        //{
        //    GroupDto groupDto;
        //    try
        //    {
        //        groupDto = await _groupService.GetGroup(id);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }

        //    return Ok(groupDto);
        //}

        //// POST: api/groups
        //[HttpPost]
        //public async Task<IActionResult> CreateGroup([FromBody] GroupDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    GroupDto result;
        //    try
        //    {
        //        result = await _groupService.CreateGroup(model);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }

        //    return Ok();
        //}

        //// PUT: api/groups
        //[HttpPut]
        //public IActionResult UpdateGroup([FromBody] GroupDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    GroupDto result;
        //    try
        //    {
        //        result = _groupService.UpdateGroup(model);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }

        //    return Ok();
        //}

    }
}

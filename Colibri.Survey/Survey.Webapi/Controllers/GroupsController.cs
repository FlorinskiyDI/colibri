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

namespace Survey.Webapi.Controllers
{
    [Authorize()]
    [Route("api/groups")]
    public class GroupsController : BaseController
    {
        private readonly IGroupService _groupService;
        public GroupsController(
            IConfiguration configuration,
            ITypeHelperService typeHelperService,
            //
            IGroupService groupService
        ) : base(configuration, typeHelperService)
        {
            _groupService = groupService;
        }

        // GET: api/groups
        [HttpGet]
        public async Task<IActionResult> GetGroupList([FromQuery] string fields)
        {
            if (!_typeHelperService.TypeHasProperties<GroupDto>(fields))
            {
                return BadRequest();
            }

            IEnumerable<GroupDto> result;
            try
            {
                result = await _groupService.GetGroupList();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(result.ShapeData(fields));
        }

        // GET: api/groups
        [HttpGet]
        [Route("root")]
        public async Task<IActionResult> GetGroupListRoot([FromQuery] string fields)
        {
            if (!_typeHelperService.TypeHasProperties<GroupDto>(fields))
            {
                return BadRequest();
            }

            IEnumerable<GroupDto> result;
            try
            {
                result = await _groupService.GetGroupListRoot();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(result.ShapeData(fields));
        }

        // GET: api/groups/1/subgroups
        [HttpGet]
        [Route("{id}/subgroups")]
        public async Task<IActionResult> GetSubGroupList(Guid id)
        {
            IEnumerable<GroupDto> result;
            try
            {
                result = await _groupService.GetSubGroupList(id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(result);
        }

        // GET: api/groups/1
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            try
            {
                var result = await _groupService.DeleteGroup(id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        // GET: api/groups/1
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGroup(Guid id)
        {
            GroupDto groupDto;
            try
            {
                groupDto = await _groupService.GetGroup(id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(groupDto);
        }

        // POST: api/groups/
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] GroupDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            GroupDto result;
            try
            {
                result = await _groupService.CreateGroup(model);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        // POST: api/groups/
        [HttpPut]
        public IActionResult UpdateGroup([FromBody] GroupDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            GroupDto result;
            try
            {
                result = _groupService.UpdateGroup(model);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

    }
}

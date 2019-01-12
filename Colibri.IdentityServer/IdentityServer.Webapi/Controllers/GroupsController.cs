using System;
using System.Linq;
using System.Threading.Tasks;
using dataaccesscore.EFCore.Paging;
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
    [Authorize]
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
        private readonly IAppUserGroupRepository _appUserGroupRepository;

        public GroupsController(
            IGroupService groupServices,
            IGroupRepository groupRepository,
            IAppUserGroupRepository appUserGroupRepository,
            IDataPager<Groups> pager
        )
        {
            _groupRepository = groupRepository;
            _groupRepository = groupRepository;
            _appUserGroupRepository = appUserGroupRepository;
            _groupServices = groupServices;
            _pager = pager;
        }

        // POST: api/groups/search
        [HttpPost("search")]
        public async Task<IActionResult> GetGroups([FromBody] SearchQuery searchEntry)
        {
            var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            var result = await _groupServices.GetAllAsync(_userId, searchEntry);
            return Ok(result);
        }


        // POST: api/groups/root
        [HttpPost("root")]
        public async Task<IActionResult> GetRootGroups([FromBody] SearchQuery searchEntry)
        {
            var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            var result = await _groupServices.GetRootAsync(_userId, searchEntry);
            return Ok(result);
        }

        // POST: api/groups/{id}/subgroups
        [HttpPost("{id}/subgroups")]
        public async Task<IActionResult> GetSubGroups([FromBody] SearchQuery searchEntry, string id)
        {
            var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            var result = await _groupServices.GetByParentIdAsync(_userId, searchEntry, id);
            return Ok(result);
        }

        // POST: api/groups
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] GroupDto model)
        {
            var userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            var result = await _groupServices.CreateGroup(model, userId);
            return Ok(result);
        }

        // DELETE: api/groups/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(string id)
        {
            var userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            await _groupServices.DeleteGroup(id, userId);
            return Ok();
        }

        // PUT: api/groups/{id}/subgroups
        [HttpPut()]
        public async Task<IActionResult> UpdateGroups([FromBody] GroupDto model)
        {
            var userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            var result = await _groupServices.UpdateGroup(model, userId);
            return Ok(result);
        }

        //private List<Groups> recursiveTreeSearch(Groups group, List<Groups> siblings)
        //{
        //    if (group.InverseParent.Count() > 0)
        //    {
        //        var cc = group.InverseParent;
        //        siblings.AddRange(cc);
        //        foreach (var item in group.InverseParent)
        //        {
        //            recursiveTreeSearch(item, siblings);
        //        }
        //    }
        //    return siblings;
        //}


        //public static T GetTfromString<T>(string mystring)
        //{
        //    var foo = TypeDescriptor.GetConverter(typeof(T));
        //    return (T)(foo.ConvertFromInvariantString(mystring));
        //}

        // GET: api/groups/{id}/subgroups
        //[HttpPost("{id}/subgroups")]
        //public async Task<IActionResult> GetSubGroups([FromBody] PageSearchEntry searchEntry, Guid id)
        //{

        //    #region test filter
        //    var columnNames = new List<string>(new string[] { "Id", "Name" });
        //    var pageNumber = 1;
        //    var pageLength = 10;

        //    var parameter = Expression.Parameter(typeof(Groups), "x");
        //    var member = Expression.Property(parameter, "Name"); //x.Name
        //    var constant = Expression.Constant("mytest1");
        //    var body = Expression.Equal(member, constant); //x.Name = "mytest1"
        //    var finalExpression = Expression.Lambda<Func<Groups, bool>>(body, parameter); //x => x.Name >= "mytest1"
        //    try
        //    {
        //        //Type myType = typeof(Groups).GetProperty("Name").PropertyType;
        //        //MethodInfo method = typeof(GroupsController).GetMethod("GetTfromString");
        //        //MethodInfo generic = method.MakeGenericMethod(myType);
        //        //var ccc = generic.Invoke(this, new[] { "03.03.1993" });

        //        var filtertest = new ExpressionBuilder.Generics.Filter<Groups>();
        //        filtertest.By("id", Operation.EqualTo, new Guid("5d35f7d0-4e5c-e811-9c5c-d017c2aa438d"), Connector.Or);
        //        filtertest.By("id", Operation.EqualTo, new Guid("c8b7e8b8-d4c0-e811-9c7c-d017c2aa438d"));
        //        var filter = new Filter<Groups>(filtertest);
        //        var results = await _pager.QueryAsync(pageNumber, pageLength, filter);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    #endregion



        //    var list = await _groupRepository.GetSubGroupsAsync(id);
        //    return Ok(list);
        //}

        // GET: api/groups/{id}
        [HttpGet("{id}")]
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



        //// POST: api/groups
        //[HttpPost]
        //public async Task<IActionResult> CreateGroup([FromBody] Groups model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    var claims = this.HttpContext.User.Claims;
        //    var userId = claims.First(c => c.Type == "sub").Value;
        //    Groups group;
        //    try
        //    {
        //        group = await _groupRepository.CreateGroupAsync(model);
        //        // if the parent has not been set, specify the user as an owner
        //        if (model.ParentId == null)
        //        {
        //            await _appUserGroupRepository.CreateAppUserGroupAsync(new ApplicationUserGroups()
        //            {
        //                GroupId = group.Id,
        //                UserId = userId
        //            });
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //    return Ok(group);
        //}

        // PUT: api/groups
        //[HttpPut]
        //public IActionResult UpdateGroup([FromBody] Groups model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    var claims = this.HttpContext.User.Claims;
        //    var userId = claims.First(c => c.Type == "sub").Value;
        //    Groups group;
        //    try
        //    {
        //        group = _groupRepository.UpdateGroup(model);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //    return Ok(group);
        //}

    }

}
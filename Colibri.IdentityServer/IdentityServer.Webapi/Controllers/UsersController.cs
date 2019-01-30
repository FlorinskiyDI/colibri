using IdentityServer.Webapi.Configurations;
using IdentityServer.Webapi.Dtos.Search;
using IdentityServer.Webapi.Dtos.Users;
using IdentityServer.Webapi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServer.Webapi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUserService _appUserService;
        public UsersController(
            IUserService appUserService
        )
        {
            _appUserService = appUserService;
        }

        // POST: api/users/search
        [HttpPost("search")]
        [Authorize(Policy = SystemStaticPermissions.Users.List)]
        public async Task<IActionResult> GetUsers([FromBody] SearchQuery searchEntry)
        {
            var result = await _appUserService.GetSearchData(searchEntry);
            return Ok(result);
        }


        // GET: api/users/getIamPolicy
        [HttpGet("iamPolicy")]
        //[Authorize(Policy = SystemStaticPermissions.Users.GetIamPolicy)]
        public async Task<IActionResult>GetIamPolicy()
        {
            var result = new List<string>{ "Admin", "GroupCreator" }; //TODO: get role (db)
            return Ok(result);
        }


        // POST: api/users/iamPolicy
        [HttpPost("iamPolicy")]
        public async Task<IActionResult> SetIamPolicy([FromBody] UserPolicyDto model)
        {
            //var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            await _appUserService.SetPolicy(model);
            return Ok();
        }

        //// GET: api/users/1/invite
        //[HttpGet("{id}/invite")]
        //public async Task<IActionResult> SendInvite(string id)
        //{
        //    //var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
        //    await _appUserService.SendInvitationByEmailConfirmationToken(id);
        //    return Ok();
        //}

        //// GET: api/users/1
        //[HttpGet("{id}/full")]
        //public async Task<IActionResult> GetFull(string id)
        //{
        //    //var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
        //    var result = await _appUserService.GetUserFullDetails(id);
        //    return Ok(result);
        //}

        //// POST: api/users/invite
        //[HttpPost("invite")]
        //public async Task<IActionResult> InviteToSystem([FromBody] UserInviteDto model)
        //{
        //    //var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
        //    //var result = await _appUserService.GetUserFullDetails(id);
        //    return Ok();
        //}

    }
}

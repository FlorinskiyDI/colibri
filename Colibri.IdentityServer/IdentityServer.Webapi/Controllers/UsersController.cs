using IdentityServer.Webapi.Dtos.Search;
using IdentityServer.Webapi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServer.Webapi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "user")]
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IAppUserService _appUserService;
        public UsersController(
            IAppUserService appUserService
        ) {
            _appUserService = appUserService;
        }

        // POST: api/users/search
        [HttpPost("search")]
        public async Task<IActionResult> GetUsers([FromBody] SearchQuery searchEntry)
        {
            //var _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
            var result = await _appUserService.GetSearchData(searchEntry);
            return Ok(result);
        }

    }
}

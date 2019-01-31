using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Webapi.Configurations;
using IdentityServer.Webapi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServer.Webapi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/groups/roles")]
    public class GroupsRolesController : Controller
    {
        // GET: api/groups/roles
        [HttpGet]
        [Authorize(Policy = SystemStaticPermissions.Groups.GetRoles)]
        public IActionResult Get()
        {
            var roles = ClassHelper.GetConstantValues<SystemRoleScopes.Groups>();
            return Ok(roles);
        }

    }
}

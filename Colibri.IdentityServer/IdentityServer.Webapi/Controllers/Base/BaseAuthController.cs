using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServer.Webapi.Controllers.Base
{
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "user")]
    public abstract class BaseAuthController : Controller
    {
        public readonly String _userId;
        public BaseAuthController()
        {
            _userId = this.HttpContext.User.Claims.First(c => c.Type == "sub").Value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Survey.Webapi.Services.TypeHelper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Survey.Webapi.Controllers.Base
{
    public class BaseController : Controller
    {
        protected readonly IConfiguration _configuration;
        protected readonly ITypeHelperService _typeHelperService;

        public BaseController(
             IConfiguration configuration,
             ITypeHelperService typeHelperService
        )
        {
            _configuration = configuration;
            _typeHelperService = typeHelperService;
        }
    }
}

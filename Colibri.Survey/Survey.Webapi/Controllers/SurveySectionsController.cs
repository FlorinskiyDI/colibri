using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Survey.ApplicationLayer.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Survey.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class SurveySectionsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ISurveySectionService _surveySectionService;

        public SurveySectionsController(
            IConfiguration configuration,
            ISurveySectionService surveySectionService
        )
        {
            _configuration = configuration;
            _surveySectionService = surveySectionService;
        }


        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var result = _surveySectionService.GetAll();
            return new string[] { "value1", "value2" };
        }
        
    }
}

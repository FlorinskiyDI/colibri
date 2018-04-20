using System;
using System.Diagnostics;
using ManagementPortal.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Survey.Spa;

namespace ManagementPortal.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;
        private readonly IOptionsSnapshot<SpaSettings> _settings;
        private readonly IHostingEnvironment _env;
        public HomeController(
            IHostingEnvironment env,
            IOptionsSnapshot<SpaSettings> settings,
            IConfiguration configuration
        ) {
            _env = env;
            _settings = settings;
            _configuration = configuration;
        }

        public IActionResult Index()
        {            
            var identityServerApiUrl = _configuration["SpaSettings:IdentityServerApiUrl"];
            _settings.Value.IdentityServerApiUrl = !string.IsNullOrEmpty(identityServerApiUrl) ? identityServerApiUrl : null;
            var serveyApiUrl = _configuration["SpaSettings:ServeyApiUrl"];
            _settings.Value.ServeyApiUrl = !string.IsNullOrEmpty(serveyApiUrl) ? serveyApiUrl : null;
            ViewBag.ServerSettings = JsonConvert.SerializeObject(_settings.Value);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

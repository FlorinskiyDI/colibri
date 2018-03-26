using System;
using System.Diagnostics;
using ManagementPortal.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Survey.Spa;

namespace ManagementPortal.Controllers
{
    public class HomeController : Controller
    {

        private readonly IOptionsSnapshot<AppSettings> _settings;
        private readonly IHostingEnvironment _env;
        public HomeController(IHostingEnvironment env, IOptionsSnapshot<AppSettings> settings)
        {
            _env = env;
            _settings = settings;
        }

        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ALARM_API_URL")))
            {
                _settings.Value.AlarmApiUrl = Environment.GetEnvironmentVariable("ALARM_API_URL");
            }

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PORTAL_VERSION")))
            {
                _settings.Value.PortalVersion = Environment.GetEnvironmentVariable("PORTAL_VERSION");
            }

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

        public IActionResult Configuration()
        {
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ALARM_API_URL")))
            {
                _settings.Value.AlarmApiUrl = Environment.GetEnvironmentVariable("ALARM_API_URL");
            }

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PORTAL_VERSION")))
            {
                _settings.Value.PortalVersion = Environment.GetEnvironmentVariable("PORTAL_VERSION");
            }

            return Json(_settings.Value);
        } 

    }    
}

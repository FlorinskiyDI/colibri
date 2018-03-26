using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ManagementPortal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseUrls("http://*:53458", "http://127.0.0.1:53458")
                .UseStartup<Startup>()
                .Build();
    }
}

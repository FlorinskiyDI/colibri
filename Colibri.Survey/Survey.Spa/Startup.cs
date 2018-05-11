
using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Survey.Spa;

namespace ManagementPortal
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var userName = (Environment.UserName ?? string.Empty).Replace(".", "-");
            if (string.IsNullOrWhiteSpace(userName))
                userName = "docker";

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile($"appsettings.{userName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.Configure<SpaSettings>(Configuration);
            services.AddMvc();

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Fastest);
            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder application,
            IHostingEnvironment hostingEnvironment,
            IServiceProvider serviceProvider,
            ILoggerFactory loggerfactory,
            IApplicationLifetime applicationLifetime)
        {
            application.UseCors("CorsPolicy");

            if (hostingEnvironment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
                application.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                   HotModuleReplacement = true,
                //    HotModuleReplacementEndpoint = "/dist/__webpack_hmr"
                });
                application.UseBrowserLink();
            }
            else
            {
                application.UseExceptionHandler("/Home/Error");
            }

            application.UseResponseCompression();
            application.UseStaticFiles();

            application.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                    
                routes.MapRoute(
                    name: "spa-fallback",
                    template: "{*url}",
                    defaults: new { controller = "Home", action = "Index" });
                });
        }
    }
}

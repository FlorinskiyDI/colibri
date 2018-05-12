using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Survey.Webapi
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddMvc()
            //.AddControllersAsServices()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddCors();

            var authority = Configuration["IdentityServer:Url"];
            var apiName = Configuration["IdentityServer:ApiResource:ApiName"];
            var apiSecret = Configuration["IdentityServer:ApiResource:ApiSecret"];
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                 .AddIdentityServerAuthentication(options =>
                 {
                     options.Authority = authority;
                     options.ApiName = apiName;
                     options.ApiSecret = apiSecret;
                 });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            //app.UseCors("corsGlobalPolicy");
            //app.UseCors(builder =>builder
            //    .AllowAnyOrigin()
            //    .AllowAnyHeader()
            //    .AllowAnyMethod()
            //    .AllowCredentials()
            //);
            app.UseCors( options => options
                .WithOrigins("http://localhost:8082")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
           );
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes
                .MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

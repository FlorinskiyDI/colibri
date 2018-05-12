using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Survey.DomainModelLayer.Entities;
using Survey.InfrastructureLayer.Context;

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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));



            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            //services.AddMvc(config =>
            //{
            //    //var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            //    //config.Filters.Add(new AuthorizeFilter(policy));
            //})

            //.AddControllersAsServices()
            services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddCors();
            //services.AddAuthorization();
            var authority = Configuration["IdentityServer:Url"];
            var apiName = Configuration["IdentityServer:ApiResource:ApiName"];
            var apiSecret = Configuration["IdentityServer:ApiResource:ApiSecret"];
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                 .AddIdentityServerAuthentication(options =>
                 {
                     options.Authority = authority;
                     options.ApiName = apiName;
                     options.ApiSecret = apiSecret;
                     options.RequireHttpsMetadata = false;
                 });

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //     .AddDefaultTokenProviders();
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

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            SqlConnectionFactory.ConnectionString = connectionString;



            app.UseCors(options => options
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

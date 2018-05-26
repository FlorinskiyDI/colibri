using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Survey.InfrastructureLayer.Context;
using AutoMapper;
using Survey.ApplicationLayer.Configurations;
using Survey.InfrastructureLayer.Configurations;
using Survey.Webapi.Configurations;
using System;

namespace Survey.Webapi
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            // appsettings
            var userName = Environment.UserName.Replace(".", "-");
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            if (!string.IsNullOrWhiteSpace(userName))
            {
                builder.AddJsonFile($"appsettings.{userName}.json", true);
            }

            _environment = env;

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //     .AddDefaultTokenProviders();

            services.AddMvc(config =>
            {
                //var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                //config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddInfrastructureDependencies(Configuration.GetConnectionString("DefaultConnection"));
            services.AddApplicationDependencies();
            services.AddWebApiDependencies();

            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters(); 

            services.AddMvc()
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
                    options.RequireHttpsMetadata = false;
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

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            SqlConnectionFactory.ConnectionString = connectionString;



            app.UseCors(options => options
               .WithOrigins("http://localhost:8082")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
           );

            
            app.UseAuthentication();

            //app.UseContextMiddleware();
            app.UseProfileMiddleware();

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

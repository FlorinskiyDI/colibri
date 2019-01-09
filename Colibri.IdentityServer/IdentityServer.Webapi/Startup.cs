using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Services;
using IdentityServer4.Services;
using IdentityServer.Webapi.Integration;
using IdentityServer.Webapi.Configurations;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4.AccessTokenValidation;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using IdentityServer.Webapi.Extensions;
using IdentityServer4.Validation;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using dataaccesscore.EFCore;
using IdentityServer.Webapi.Configurations.AspNetIdentity;

namespace IdentityServer.Webapi
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;

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

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //var cert = new X509Certificate2(Path.Combine(_environment.ContentRootPath, "damienbodserver.pfx"), "");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddStorageCoreDataAccess<ApplicationDbContext>();


            // configure expiring token of  EmailConfirmationToken
            var emailConfirmTokenProviderName = Configuration.GetSection("TokenProviders").GetSection("EmailConfirmTokenProvider").GetValue<string>("Name");
            var emailConfirmTokenProviderTokenLifespan = Configuration.GetSection("TokenProviders").GetSection("EmailConfirmTokenProvider").GetValue<double>("TokenLifespan");
            services.Configure<IdentityOptions>(options => options.Tokens.EmailConfirmationTokenProvider = emailConfirmTokenProviderName);
            services.Configure<EmailConfirmProtectorTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromMilliseconds(emailConfirmTokenProviderTokenLifespan));


            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<EmailConfirmDataProtectorTokenProvider<ApplicationUser>>(emailConfirmTokenProviderName);

            var policy = new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicy();
            policy.Headers.Add("*");
            policy.Methods.Add("*");
            policy.Origins.Add("*");
            policy.SupportsCredentials = true;

            services.AddCors(x => x.AddPolicy("corsGlobalPolicy", policy));

            var guestPolicy = new AuthorizationPolicyBuilder()
               .RequireAuthenticatedUser()
               .RequireClaim("scope", "dataEventRecords")
               .Build();



            

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Resources.GetIdentityResources())
                .AddInMemoryApiResources(Resources.GetApiResources())
                .AddInMemoryClients(Clients.GetClients())
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<IdentityProfileService>();

            services.AddTransient<IProfileService, IdentityProfileService>();
            services.AddTransient<IExtensionGrantValidator, DelegationGrantValidator>();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDependencies(Configuration, connectionString);
            SqlConnectionFactory.ConnectionString = connectionString;

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
              .AddIdentityServerAuthentication(options =>
              {
                  options.Authority = "http://localhost:5050" + "/";
                  options.ApiName = "api2";
                  options.ApiSecret = "secret";
                  options.SupportedTokens = SupportedTokens.Both;
                  options.RequireHttpsMetadata = false;
              });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("dataEventRecordsAdmin", policyAdmin =>
                {
                    policyAdmin.RequireClaim("role", "dataEventRecords.admin");
                });
                options.AddPolicy("admin", policyAdmin =>
                {
                    policyAdmin.RequireClaim("role", "admin");
                });
                options.AddPolicy("user", policyUser =>
                {
                    policyUser.RequireClaim("role", "user");
                });
                options.AddPolicy("dataEventRecords", policyUser =>
                {
                    policyUser.RequireClaim("scope", "dataEventRecords");
                });
            });

            services.AddMvc().AddJsonOptions(options =>
             {
                 options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
             });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            

            //var connectionString = Configuration.GetConnectionString("DefaultConnection");
            //SqlConnectionFactory.ConnectionString = connectionString;

            //app.UseCors(options => options
            //   //.WithOrigins("http://localhost:5151")
            //   .AllowAnyOrigin()
            //   .AllowAnyHeader()
            //   .AllowAnyMethod()
            //   .AllowCredentials()
            //);
            app.UseCors("corsGlobalPolicy");

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

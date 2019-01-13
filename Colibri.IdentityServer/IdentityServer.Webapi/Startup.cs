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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //var cert = new X509Certificate2(Path.Combine(_environment.ContentRootPath, "damienbodserver.pfx"), "");

            //services.AddTransient<UserManager<ApplicationUser>>();
            //services.AddTransient<RoleManager<ApplicationRole>>();
            //services.AddTransient<SignInManager<ApplicationRole>>();




            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddStorageCoreDataAccess<ApplicationDbContext>();


            // configure expiring token of  EmailConfirmationToken
            var emailConfirmTokenProviderName = Configuration.GetSection("TokenProviders").GetSection("EmailConfirmTokenProvider").GetValue<string>("Name");
            var emailConfirmTokenProviderTokenLifespan = Configuration.GetSection("TokenProviders").GetSection("EmailConfirmTokenProvider").GetValue<double>("TokenLifespan");
            services.Configure<IdentityOptions>(options => options.Tokens.EmailConfirmationTokenProvider = emailConfirmTokenProviderName);
            services.Configure<EmailConfirmProtectorTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromMilliseconds(emailConfirmTokenProviderTokenLifespan));

            services.AddScoped<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid, IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>, IdentityUserToken<Guid>, IdentityRoleClaim<Guid>>, ApplicationUserStore>();
            services.AddScoped<UserManager<ApplicationUser>, ApplicationUserManager>();
            //services.AddScoped<ApplicationUserStore, ApplicationUserStore>();
            services.AddScoped<ApplicationUserManager, ApplicationUserManager>();

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserStore<ApplicationUserStore>()
                .AddUserManager<ApplicationUserManager>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<EmailConfirmDataProtectorTokenProvider<ApplicationUser>>(emailConfirmTokenProviderName);

            var corsPolicy = new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicy();
            corsPolicy.Headers.Add("*");
            corsPolicy.Methods.Add("*");
            corsPolicy.Origins.Add("*");
            corsPolicy.SupportsCredentials = true;

            services.AddCors(x => x.AddPolicy("corsGlobalPolicy", corsPolicy));

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
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();

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
                // Note: It is policy for Authorize atribute
                options.DefaultPolicy = new AuthorizationPolicyBuilder(new[] { JwtBearerDefaults.AuthenticationScheme, IdentityConstants.ApplicationScheme })
                .RequireAuthenticatedUser()
                .Build();
                // Note: Adding permisions as policy
                var permissionList = SystemPermissionScope.GetValues();
                foreach (var permission in permissionList)
                {
                    options.AddPolicy(
                        permission,
                        policy => { policy.Requirements.Add(new PermissionRequirement(permission)); });
                }
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

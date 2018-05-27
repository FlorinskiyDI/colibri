using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IdentityServer.Webapi.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSenderService _emailSenderService;
        public AppUserService(
            UserManager<ApplicationUser> userManager,
            IEmailSenderService emailSenderService
        )
        {
            this._userManager = userManager;
            _emailSenderService = emailSenderService;
        }

        public async Task<ApplicationUser> AddUserByEmailWithoutPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser { UserName = email, Email = email };
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    throw new ArgumentException("The app user was not created");
                }
                // send invite to user
                var confirmationToken = await GetEmailConfirmationToken(email);
                string codeHtmlVersion = HttpUtility.UrlEncode(confirmationToken);
                var confirmationUrl = $@"http://localhost:5050/Account/RegisterByEmail/?userId={ user.Id }&code={ codeHtmlVersion }";
                await _emailSenderService.SendAccountConfirmationEmailAsync(null, email, "Confirm your account", confirmationUrl);
            }
            //
            return user;
        }

        public async Task<string> GetEmailConfirmationToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //
            return token;
        }

    }
}

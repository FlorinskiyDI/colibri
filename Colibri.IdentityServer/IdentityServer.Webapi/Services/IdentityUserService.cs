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
    public class IdentityUserService : IIdentityUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISiteEmailMessageSender _siteEmailMessageSender;
        public IdentityUserService(
            UserManager<ApplicationUser> userManager,
            ISiteEmailMessageSender siteEmailMessageSender
        )
        {
            this._userManager = userManager;
            this._siteEmailMessageSender = siteEmailMessageSender;
        }


        public async Task<bool> AddMembersFroupAsync(Guid groupId, List<string> emailList)
        {
            foreach (var email in emailList)
            {
                var identityUser = await AddIdentityUser(email);
                var confirmationToken = await GetEmailConfirmationToken(email);
                string codeHtmlVersion = HttpUtility.UrlEncode(confirmationToken);
                var confirmationUrl = $@"http://localhost:5050/Account/RegisterByEmail/?userId={ identityUser.Id }&code={ codeHtmlVersion }";                
                await _siteEmailMessageSender.SendAccountConfirmationEmailAsync(null, email, "Confirm your account", confirmationUrl);
            }
            //
            return true;
        }

        public async Task<ApplicationUser> AddIdentityUser(string email)
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
            }
            //
            return user;
        }

        public async Task<string> GetEmailConfirmationToken(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                return emailConfirmationToken;
            }
            catch (Exception e)
            {

                throw;
            }

        }
    }
}

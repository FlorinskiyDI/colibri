using IdentityServer.Webapi.Infrastructure.Messaging;
using IdentityServer.Webapi.Infrastructure.Razor;
using IdentityServer.Webapi.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private IConfiguration _configuration;
        private IViewRenderResolver _viewRenderResolver;
        private IEmailSender _emailSender;

        public EmailSenderService(
            IViewRenderResolver viewRenderResolver,
            IConfiguration configuration,
            IEmailSender emailSender
        )
        {
            _viewRenderResolver = viewRenderResolver;
            _configuration = configuration;
            _emailSender = emailSender;
        }


        public async Task SendAccountConfirmationEmailAsync(object siteSettings, string toAddress, string subject, string confirmationUrl)
        {
            var smtpOptions = await GetSmptOptions().ConfigureAwait(false);
            if (smtpOptions == null)
            {
                var logMessage = $"failed to send account confirmation email because smtp settings are not populated for site";
                return;
            }

            try
            {
                var plainTextMessage = await _viewRenderResolver.RenderToStringAsync("EmailTemplates/ConfirmAccountTextEmail", confirmationUrl).ConfigureAwait(false);
                var htmlMessage = await _viewRenderResolver.RenderToStringAsync("EmailTemplates/ConfirmAccountHtmlEmail", confirmationUrl).ConfigureAwait(false);

                //await _emailSender.SendEmailAsync(
                //    smtpOptions,
                //    toAddress,
                //    smtpOptions.DefaultEmailFromAddress,
                //    subject,
                //    plainTextMessage,
                //    htmlMessage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //log.LogError("error sending account confirmation email", ex);
            }

        }

        public async Task SendPasswordResetEmailAsync(object siteSettings, string toAddress, string subject, string resetUrl)
        {
            var smtpOptions = await GetSmptOptions().ConfigureAwait(false);
            if (smtpOptions == null)
            {
                var logMessage = $"failed to send password reset email because smtp settings are not populated for site";
                return;
            }

            try
            {
                var plainTextMessage = await _viewRenderResolver.RenderToStringAsync("EmailTemplates/PasswordResetTextEmail", resetUrl);
                var htmlMessage = await _viewRenderResolver.RenderToStringAsync("EmailTemplates/PasswordResetHtmlEmail", resetUrl);
                await _emailSender.SendEmailAsync(
                    smtpOptions,
                    toAddress,
                    smtpOptions.DefaultEmailFromAddress,
                    subject,
                    plainTextMessage,
                    htmlMessage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //log.LogError("error sending password reset email", ex);
            }
        }

        //
        private async Task<SmtpOptions> GetSmptOptions()
        {
            SmtpOptions smtpOptions = new SmtpOptions();
            smtpOptions.Server = _configuration["SmtpOptions:Server"];
            smtpOptions.Port = Int32.Parse(_configuration["SmtpOptions:Port"]);
            smtpOptions.RequiresAuthentication = Boolean.Parse(_configuration["SmtpOptions:RequiresAuthentication"]);
            smtpOptions.User = _configuration["SmtpOptions:User"];
            smtpOptions.Password = _configuration["SmtpOptions:Password"];
            smtpOptions.DefaultEmailFromAddress = _configuration["SmtpOptions:DefaultEmailFromAddress"];
            //smtpOptions.PreferredEncoding = siteSettings.SmtpPreferredEncoding;
            smtpOptions.UseSsl = Boolean.Parse(_configuration["SmtpOptions:UseSsl"]);
            //
            return await Task.FromResult(smtpOptions);
        }
    }
}

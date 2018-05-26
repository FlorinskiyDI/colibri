using IdentityServer.Webapi.Infrastructure.Messaging;
using IdentityServer.Webapi.Infrastructure.Razor;
using IdentityServer.Webapi.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services
{
    public class SiteEmailMessageSender : ISiteEmailMessageSender
    {
        private IViewRenderService _viewRenderService;
        //private ILogger log;

        public SiteEmailMessageSender(
            IViewRenderService viewRenderService
            //ILogger<SiteEmailMessageSender> logger
            )
        {
            //log = logger;
            _viewRenderService = viewRenderService;

        }

        private async Task<SmtpOptions> GetSmptOptions()
        {
            SmtpOptions smtpOptions = new SmtpOptions();
            smtpOptions.Password = "FlorinskyDmitriy";
            smtpOptions.Port = 587;
            //smtpOptions.PreferredEncoding = siteSettings.SmtpPreferredEncoding;
            smtpOptions.RequiresAuthentication = true;
            smtpOptions.Server = "smtp.gmail.com";
            smtpOptions.User = "dmytro.florynskyi@gmail.com";
            //smtpOptions.UseSsl = siteSettings.SmtpUseSsl;
            smtpOptions.DefaultEmailFromAddress = "dmytro.florynskyi@gmail.com";

            return await Task.FromResult(smtpOptions);
        }

        public async Task SendAccountConfirmationEmailAsync(
            //ISiteContext siteSettings,
            object siteSettings,
            string toAddress,
            string subject,
            string confirmationUrl
        )
        {
            var smtpOptions = await GetSmptOptions().ConfigureAwait(false);
            if (smtpOptions == null)
            {
                var logMessage = $"failed to send account confirmation email because smtp settings are not populated for site";
                //log.LogError(logMessage);
                return;
            }

            var sender = new EmailSender();
            try
            {
                var plainTextMessage = await _viewRenderService.RenderToStringAsync("EmailTemplates/ConfirmAccountTextEmail", confirmationUrl).ConfigureAwait(false);
                var htmlMessage = await _viewRenderService.RenderToStringAsync("EmailTemplates/ConfirmAccountHtmlEmail", confirmationUrl).ConfigureAwait(false);

                await sender.SendEmailAsync(
                    smtpOptions,
                    toAddress,
                    smtpOptions.DefaultEmailFromAddress,
                    subject,
                    plainTextMessage,
                    htmlMessage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //log.LogError("error sending account confirmation email", ex);
            }

        }

        public async Task SendPasswordResetEmailAsync(
            object siteSettings,
            string toAddress,
            string subject,
            string resetUrl)
        {
            var smtpOptions = await GetSmptOptions().ConfigureAwait(false);

            if (smtpOptions == null)
            {
                //var logMessage = $"failed to send password reset email because smtp settings are not populated for site {siteSettings.SiteName}";
                var logMessage = $"failed to send password reset email because smtp settings are not populated for site";
                //log.LogError(logMessage);
                return;
            }

            var sender = new EmailSender();
            // in account controller we are calling this method without await
            // so it doesn't block the UI. Which means it is running on a background thread
            // similar as the old ThreadPool.QueueWorkItem
            // as such we need to handle any error that may happen so it doesn't
            // brind down the thread or the process
            try
            {
                var plainTextMessage
                   = await _viewRenderService.RenderToStringAsync("EmailTemplates/PasswordResetTextEmail", resetUrl);

                var htmlMessage
                    = await _viewRenderService.RenderToStringAsync("EmailTemplates/PasswordResetHtmlEmail", resetUrl);

                await sender.SendEmailAsync(
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

    }
}

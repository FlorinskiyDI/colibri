using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Infrastructure.Messaging
{
    public class EmailSender: IEmailSender
    {
        public EmailSender()
        {
        }

        public async Task SendEmailAsync(
            SmtpOptions smtpOptions,
            string to,
            string from,
            string subject,
            string plainTextMessage,
            string htmlMessage,
            string replyTo = null,
            Importance importance = Importance.Normal
            )
        {
            if (string.IsNullOrWhiteSpace(to))
            {
                throw new ArgumentException("no to address provided");
            }

            if (string.IsNullOrWhiteSpace(from))
            {
                throw new ArgumentException("no from address provided");
            }

            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentException("no subject provided");
            }

            var hasPlainText = !string.IsNullOrWhiteSpace(plainTextMessage);
            var hasHtml = !string.IsNullOrWhiteSpace(htmlMessage);
            if (!hasPlainText && !hasHtml)
            {
                throw new ArgumentException("no message provided");
            }

            var m = new MimeMessage();

            m.From.Add(new MailboxAddress("", from));
            if (!string.IsNullOrWhiteSpace(replyTo))
            {
                m.ReplyTo.Add(new MailboxAddress("", replyTo));
            }
            m.To.Add(new MailboxAddress("", to));
            m.Subject = subject;
            m.Importance = GetMessageImportance(importance);

            //Header h = new Header(HeaderId.Precedence, "Bulk");
            //m.Headers.Add()

            BodyBuilder bodyBuilder = new BodyBuilder();
            if (hasPlainText)
            {
                bodyBuilder.TextBody = plainTextMessage;
            }

            if (hasHtml)
            {
                bodyBuilder.HtmlBody = htmlMessage;
            }

            m.Body = bodyBuilder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(
                        smtpOptions.Server,
                        smtpOptions.Port,
                        smtpOptions.UseSsl)
                        .ConfigureAwait(false);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    if (smtpOptions.RequiresAuthentication)
                    {
                        await client.AuthenticateAsync(smtpOptions.User, smtpOptions.Password)
                            .ConfigureAwait(false);
                    }

                    await client.SendAsync(m).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SendMultipleEmailAsync(
            SmtpOptions smtpOptions,
            string toCsv,
            string from,
            string subject,
            string plainTextMessage,
            string htmlMessage,
            string replyTo = null,
            Importance importance = Importance.Normal
            )
        {
            if (string.IsNullOrWhiteSpace(toCsv))
            {
                throw new ArgumentException("no to addresses provided");
            }

            if (string.IsNullOrWhiteSpace(from))
            {
                throw new ArgumentException("no from address provided");
            }

            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentException("no subject provided");
            }

            var hasPlainText = !string.IsNullOrWhiteSpace(plainTextMessage);
            var hasHtml = !string.IsNullOrWhiteSpace(htmlMessage);
            if (!hasPlainText && !hasHtml)
            {
                throw new ArgumentException("no message provided");
            }

            var m = new MimeMessage();

            m.From.Add(new MailboxAddress("", from));
            if (!string.IsNullOrWhiteSpace(replyTo))
            {
                m.ReplyTo.Add(new MailboxAddress("", replyTo));
            }

            if (toCsv.Contains(","))
            {
                string[] adrs = toCsv.Split(',');

                foreach (string item in adrs)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        m.To.Add(new MailboxAddress("", item));
                    }
                }
            }
            else
            {
                m.To.Add(new MailboxAddress("", toCsv));
            }


            m.Subject = subject;
            m.Importance = GetMessageImportance(importance);

            BodyBuilder bodyBuilder = new BodyBuilder();
            if (hasPlainText)
            {
                bodyBuilder.TextBody = plainTextMessage;
            }

            if (hasHtml)
            {
                bodyBuilder.HtmlBody = htmlMessage;
            }

            m.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(
                    smtpOptions.Server,
                    smtpOptions.Port,
                    smtpOptions.UseSsl).ConfigureAwait(false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                if (smtpOptions.RequiresAuthentication)
                {
                    await client.AuthenticateAsync(
                        smtpOptions.User,
                        smtpOptions.Password).ConfigureAwait(false);
                }

                await client.SendAsync(m).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }

        }

        private MessageImportance GetMessageImportance(Importance importance)
        {
            switch (importance)
            {
                case Importance.Low:
                    return MessageImportance.Low;
                case Importance.High:
                    return MessageImportance.High;
                case Importance.Normal:
                default:
                    return MessageImportance.Normal;
            }
        }

    }

    public enum Importance
    {
        //
        // Summary:
        //     The message is of low importance.
        Low = 0,
        //
        // Summary:
        //     The message is of normal importance.
        Normal = 1,
        //
        // Summary:
        //     The message is of high importance.
        High = 2
    }
}

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Settings;
using TokanPages.BackEnd.Shared.Models.Emails;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;

namespace TokanPages.BackEnd.SmtpClient
{

    public class SmtpClientService : SmtpClientObject, ISmtpClientService
    {

        private readonly SmtpServer FSmtpClient;

        public SmtpClientService(SmtpServer ASmtpClient)
        {
            FSmtpClient = ASmtpClient;
        }

        public override string From { get; set; }
        public override List<string> Tos { get; set; }
        public override string Subject { get; set; }
        public override string PlainText { get; set; }
        public override string HtmlBody { get; set; }

        public override async Task<ActionResult> Send()
        {

            try 
            {

                var LMail = new MimeMessage();

                LMail.From.Add(MailboxAddress.Parse(From));
                LMail.Subject = Subject;

                foreach (var Item in Tos) 
                {
                    LMail.To.Add(MailboxAddress.Parse(Item));
                }

                if (!string.IsNullOrEmpty(PlainText)) 
                    LMail.Body = new TextPart(TextFormat.Plain) { Text = PlainText };

                if (!string.IsNullOrEmpty(HtmlBody)) 
                    LMail.Body = new TextPart(TextFormat.Html) { Text = HtmlBody };

                using var LServer = new MailKit.Net.Smtp.SmtpClient();
                LServer.Connect(FSmtpClient.Server, FSmtpClient.Port, SecureSocketOptions.SslOnConnect);
                await LServer.AuthenticateAsync(FSmtpClient.Account, FSmtpClient.Password);
                await LServer.SendAsync(LMail);
                await LServer.DisconnectAsync(true);

                return new ActionResult
                {
                    IsSucceeded = true
                };

            } 
            catch (Exception LException)
            {
                return new ActionResult
                {
                    IsSucceeded = false,
                    ErrorCode = LException.HResult.ToString(),
                    ErrorDesc = LException.Message
                };
            }

        }

    }

}

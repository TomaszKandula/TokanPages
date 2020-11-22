using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Settings;
using TokanPages.BackEnd.Shared.Models.Emails;

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

                var LMail = new MailMessage();
                var LServer = new System.Net.Mail.SmtpClient(FSmtpClient.Server);

                LMail.From = new MailAddress(From);
                LMail.Subject = Subject;

                foreach (var Item in Tos) 
                {
                    LMail.To.Add(Item);
                }

                if (!string.IsNullOrEmpty(PlainText)) 
                {
                    LMail.Body = PlainText;
                    LMail.IsBodyHtml = false;
                }

                if (!string.IsNullOrEmpty(HtmlBody)) 
                {
                    LMail.Body = HtmlBody;
                    LMail.IsBodyHtml = true;
                }

                LServer.Port = FSmtpClient.Port;
                LServer.EnableSsl = FSmtpClient.IsSSL;
                LServer.Credentials = new NetworkCredential(FSmtpClient.Account, FSmtpClient.Password);

                await LServer.SendMailAsync(LMail);
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

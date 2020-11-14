using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Settings;

namespace TokanPages.BackEnd.SendGrid
{

    public class SendGridService : ISendGridService
    {

        private readonly SendGridKeys FSendGridKeys;

        public SendGridService(SendGridKeys ASendGridKeys) 
        {
            FSendGridKeys = ASendGridKeys;
        }

        public SendGridService()
        {
        }

        public virtual string From { get; set; }
        public virtual List<string> Tos { get; set; }
        public virtual string Subject { get; set; }
        public virtual string PlainText { get; set; }
        public virtual string HtmlBody { get; set; }

        public virtual async Task<Response> Send() 
        {

            var EmailTos = new List<EmailAddress>();

            foreach (var Email in Tos) 
            {
                EmailTos.Add(new EmailAddress(Email, Email));
            }

            var Client     = new SendGridClient(FSendGridKeys.ApiKey1);
            var EmailFrom  = new EmailAddress(From, From);
            var Message    = MailHelper.CreateSingleEmailToMultipleRecipients(EmailFrom, EmailTos, Subject, PlainText, HtmlBody);
        
            return await Client.SendEmailAsync(Message);
        
        }

    }

}

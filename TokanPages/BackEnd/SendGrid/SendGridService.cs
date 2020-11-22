using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Settings;

namespace TokanPages.BackEnd.SendGrid
{

    public class SendGridService : SendGridObject, ISendGridService
    {

        private readonly SendGridKeys FSendGridKeys;

        public SendGridService(SendGridKeys ASendGridKeys) 
        {
            FSendGridKeys = ASendGridKeys;
        }

        public SendGridService()
        {
        }

        public override string From { get; set; }
        public override List<string> Tos { get; set; }
        public override string Subject { get; set; }
        public override string PlainText { get; set; }
        public override string HtmlBody { get; set; }

        public override async Task<Response> Send() 
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

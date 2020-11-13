using System.Threading.Tasks;
using TokanPages.BackEnd.Shared.Models;

namespace TokanPages.BackEnd.Logic.Mailer
{

    public interface IMailer
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Task<MailerResult> Send();
        public bool FieldsCheck();
    }

}

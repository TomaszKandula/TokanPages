using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Mailer.Model;

namespace TokanPages.BackEnd.Mailer
{

    public interface IMailer
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Task<Result> Send();
        public bool FieldsCheck();
        public List<Emails> CheckEmailAddresses(List<string> AEmailAddress);
    }

}

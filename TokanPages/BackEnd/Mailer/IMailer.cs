using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Mailer.Model;

namespace TokanPages.BackEnd.Mailer
{

    public interface IMailer
    {

        public Task<Result> Send();

        public bool FieldsCheck();

        public List<Emails> CheckEmailAddresses(List<string> AEmailAddress);

    }

}

using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Logic.Mailer.Model;
using TokanPages.Backend.SmtpClient.Models;

namespace TokanPages.Logic.Mailer
{

    public interface IMailer
    {
        public string From { get; set; }
        public List<string> Tos { get; set; }
        public List<string> Ccs { get; set; }
        public List<string> Bccs { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Task<ActionResult> Send();
        public bool ValidateInputs();
        Task<string> MakeBody(string ATemplate, List<ValueTag> AValueTag);
    }

}

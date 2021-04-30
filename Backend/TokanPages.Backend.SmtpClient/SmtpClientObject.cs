using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.SmtpClient.Models;

namespace TokanPages.Backend.SmtpClient
{
    public abstract class SmtpClientObject
    {
        public abstract string From { get; set; }

        public abstract List<string> Tos { get; set; }
        
        public abstract List<string> Ccs { get; set; }
        
        public abstract List<string> Bccs { get; set; }
        
        public abstract string Subject { get; set; }
        
        public abstract string PlainText { get; set; }
        
        public abstract string HtmlBody { get; set; }
        
        public abstract Task<SendActionResult> Send();
        
        public abstract List<CheckActionResult> IsAddressCorrect(IEnumerable<string> AEmailAddress);
        
        public abstract Task<bool> IsDomainCorrect(string AEmailAddress);
    }
}

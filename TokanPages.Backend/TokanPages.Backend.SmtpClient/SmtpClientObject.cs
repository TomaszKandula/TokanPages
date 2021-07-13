namespace TokanPages.Backend.SmtpClient
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Shared.Models;
    using Models;

    public abstract class SmtpClientObject
    {
        public abstract string From { get; set; }

        public abstract List<string> Tos { get; set; }
        
        public abstract List<string> Ccs { get; set; }
        
        public abstract List<string> Bccs { get; set; }
        
        public abstract string Subject { get; set; }
        
        public abstract string PlainText { get; set; }
        
        public abstract string HtmlBody { get; set; }
        
        public abstract Task<ActionResult> CanConnectAndAuthenticate(CancellationToken ACancellationToken = default);
        
        public abstract Task<ActionResult> Send(CancellationToken ACancellationToken = default);
        
        public abstract List<EmailAddressModel> IsAddressCorrect(IEnumerable<string> AEmailAddress);
        
        public abstract Task<bool> IsDomainCorrect(string AEmailAddress, CancellationToken ACancellationToken = default);
    }
}
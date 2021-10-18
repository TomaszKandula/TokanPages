namespace TokanPages.Backend.SmtpClient
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Shared.Models;
    using Models;

    public interface ISmtpClientService
    {
        string From { get; set; }

        List<string> Tos { get; set; }
        
        List<string> Ccs { get; set; }
        
        List<string> Bccs { get; set; }
        
        string Subject { get; set; }
        
        string PlainText { get; set; }
        
        string HtmlBody { get; set; }

        Task<ActionResult> CanConnectAndAuthenticate(CancellationToken cancellationToken = default);
        
        Task<ActionResult> Send(CancellationToken cancellationToken = default);
        
        List<Email> IsAddressCorrect(IEnumerable<string> emailAddress);
        
        Task<bool> IsDomainCorrect(string emailAddress, CancellationToken cancellationToken = default);
    }
}
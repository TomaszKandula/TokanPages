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

        Task<ActionResultModel> CanConnectAndAuthenticate(CancellationToken ACancellationToken = default);
        
        Task<ActionResultModel> Send(CancellationToken ACancellationToken = default);
        
        List<EmailAddressModel> IsAddressCorrect(IEnumerable<string> AEmailAddress);
        
        Task<bool> IsDomainCorrect(string AEmailAddress, CancellationToken ACancellationToken = default);
    }
}
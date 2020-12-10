using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.SmtpClient.Models;

namespace TokanPages.Backend.SmtpClient
{

    public interface ISmtpClientService
    {
        string From { get; set; }
        List<string> Tos { get; set; }
        List<string> Ccs { get; set; }
        List<string> Bccs { get; set; }
        string Subject { get; set; }
        string PlainText { get; set; }
        string HtmlBody { get; set; }
        Task<ActionResult> Send();
    }

}

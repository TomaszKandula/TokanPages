using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Shared.Models.Emails;

namespace TokanPages.BackEnd.SmtpClient
{

    public interface ISmtpClientService
    {
        string From { get; set; }
        List<string> Tos { get; set; }
        string Subject { get; set; }
        string PlainText { get; set; }
        string HtmlBody { get; set; }
        Task<ActionResult> Send();
    }

}

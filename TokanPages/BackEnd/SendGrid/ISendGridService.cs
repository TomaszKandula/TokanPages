using System.Threading.Tasks;
using System.Collections.Generic;
using SendGrid;

namespace TokanPages.BackEnd.SendGrid
{

    public interface ISendGridService
    {
        string From { get; set; }
        List<string> Tos { get; set; }
        string Subject { get; set; }
        string PlainText { get; set; }
        string HtmlBody { get; set; }
        Task<Response> Send();
    }

}

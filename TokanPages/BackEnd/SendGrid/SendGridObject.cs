using System.Threading.Tasks;
using System.Collections.Generic;
using SendGrid;

namespace TokanPages.BackEnd.SendGrid
{

    public abstract class SendGridObject
    {
        public abstract string From { get; set; }
        public abstract List<string> Tos { get; set; }
        public abstract string Subject { get; set; }
        public abstract string PlainText { get; set; }
        public abstract string HtmlBody { get; set; }
        public abstract Task<Response> Send();
    }

}

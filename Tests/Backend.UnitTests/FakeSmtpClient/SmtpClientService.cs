using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.SmtpClient;
using TokanPages.BackEnd.Shared.Models;

namespace BackEnd.UnitTests.FakeSendGrid
{

    public class SmtpClientService : SmtpClientObject, ISmtpClientService
    {

        public SmtpClientService() 
        {        
        }

        public override string From { get; set; }
        public override List<string> Tos { get; set; }
        public override List<string> Ccs { get; set; }
        public override List<string> Bccs { get; set; }
        public override string Subject { get; set; }
        public override string PlainText { get; set; }
        public override string HtmlBody { get; set; }

        public override async Task<ActionResult> Send() 
        {

            return await Task.Run(() => 
            {

                if (!Tos.Any()) 
                {
                    return new ActionResult 
                    { 
                        IsSucceeded = false
                    };
                }

                if (string.IsNullOrEmpty(From) || string.IsNullOrEmpty(Subject)) 
                {
                    return new ActionResult
                    {
                        IsSucceeded = false
                    };
                }

                return new ActionResult
                {
                    IsSucceeded = true
                };

            });

        }

    }

}

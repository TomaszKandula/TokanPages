using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.SmtpClient.Models;

namespace Backend.UnitTests.FakeSendGrid
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

        public override async Task<SendActionResult> Send() 
        {

            return await Task.Run(() => 
            {

                if (!Tos.Any()) 
                {
                    return new SendActionResult 
                    { 
                        IsSucceeded = false
                    };
                }

                if (string.IsNullOrEmpty(From) || string.IsNullOrEmpty(Subject)) 
                {
                    return new SendActionResult
                    {
                        IsSucceeded = false
                    };
                }

                return new SendActionResult
                {
                    IsSucceeded = true
                };

            });

        }

    }

}

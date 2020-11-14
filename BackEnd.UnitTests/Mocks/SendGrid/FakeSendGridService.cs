using SendGrid;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.SendGrid;

namespace BackEnd.UnitTests.Mocks.SendGrid
{

    public class FakeSendGridService : SendGridService
    {

        public FakeSendGridService() 
        {        
        }

        public override string From { get; set; }
        public override List<string> Tos { get; set; }
        public override string Subject { get; set; }
        public override string PlainText { get; set; }
        public override string HtmlBody { get; set; }

        public override async Task<Response> Send() 
        {

            return await Task.Run(() => 
            {
                var StatusCode = HttpStatusCode.Accepted;
                var ResponseBody = new StringContent("Email sent.");
                return new Response(StatusCode, ResponseBody, null);
            });

        }

    }

}

using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.SendGrid;
using SendGrid;

namespace BackEnd.UnitTests.FakeSendGrid
{

    public class SendGridService : SendGridObject, ISendGridService
    {

        public SendGridService() 
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

                dynamic StatusCode = null;
                dynamic ResponseBody = null;

                if (!Tos.Any()) 
                {
                    StatusCode = HttpStatusCode.BadRequest;
                    ResponseBody = new StringContent("Cannot send.");
                    return new Response(StatusCode, ResponseBody, null);
                }

                if (string.IsNullOrEmpty(From) || string.IsNullOrEmpty(Subject)) 
                {
                    StatusCode = HttpStatusCode.BadRequest;
                    ResponseBody = new StringContent("Cannot send.");
                    return new Response(StatusCode, ResponseBody, null);
                }

                StatusCode = HttpStatusCode.Accepted;
                ResponseBody = new StringContent("Sent.");
                return new Response(StatusCode, ResponseBody, null);

            });

        }

    }

}

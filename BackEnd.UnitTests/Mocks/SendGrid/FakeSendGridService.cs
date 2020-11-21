using SendGrid;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TokanPages.BackEnd.SendGrid;

namespace BackEnd.UnitTests.Mocks.SendGrid
{

    public class FakeSendGridService : SendGridService
    {

        public FakeSendGridService() 
        {        
        }

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

using Xunit;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using TokanPages;
using Newtonsoft.Json;

namespace Backend.IntegrationTests.Handlers.Mailer
{

    public class SendMessageCommandHandlerTest : IClassFixture<TestFixture<Startup>>
    {

        private readonly HttpClient FHttpClient;

        public SendMessageCommandHandlerTest(TestFixture<Startup> ACustomFixture)
        {
            FHttpClient = ACustomFixture.FClient;
        }

        //...

    }

}

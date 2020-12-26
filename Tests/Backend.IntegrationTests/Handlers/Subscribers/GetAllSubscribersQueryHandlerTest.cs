using Xunit;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using TokanPages;
using Newtonsoft.Json;

namespace Backend.IntegrationTests.Handlers.Subscribers
{
 
    public class GetAllSubscribersQueryHandlerTest : IClassFixture<TestFixture<Startup>>
    {

        private readonly HttpClient FHttpClient;

        public GetAllSubscribersQueryHandlerTest(TestFixture<Startup> ACustomFixture)
        {
            FHttpClient = ACustomFixture.FClient;
        }

        //...

    }

}

using Xunit;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using TokanPages;
using Newtonsoft.Json;

namespace Backend.IntegrationTests.Handlers.Users
{

    public class GetAllUsersQueryHandlerTest : IClassFixture<TestFixture<Startup>>
    {

        private readonly HttpClient FHttpClient;

        public GetAllUsersQueryHandlerTest(TestFixture<Startup> ACustomFixture) 
        {
            FHttpClient = ACustomFixture.FClient;
        }

        //...

    }

}

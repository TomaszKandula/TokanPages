using Xunit;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using TokanPages;
using Newtonsoft.Json;

namespace Backend.IntegrationTests.Handlers.Articles
{

    public class RemoveArticleCommandHandlerTest : IClassFixture<TestFixture<Startup>>
    {

        private readonly HttpClient FHttpClient;

        public RemoveArticleCommandHandlerTest(TestFixture<Startup> ACustomFixture)
        {
            FHttpClient = ACustomFixture.FClient;
        }

        //...

    }

}

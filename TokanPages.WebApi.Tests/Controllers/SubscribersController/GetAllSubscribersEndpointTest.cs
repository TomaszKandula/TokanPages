namespace TokanPages.WebApi.Tests.Controllers.SubscribersController
{
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;

    public class GetAllSubscribersEndpointTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/subscribers";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public GetAllSubscribersEndpointTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;
        
        [Fact]
        public async Task GivenNoJwt_WhenGetAllSubscribers_ShouldReturnUnauthorized()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/GetAllSubscribers/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.Unauthorized);
        }
    }
}
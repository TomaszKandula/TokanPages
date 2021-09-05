namespace TokanPages.WebApi.Tests.Controllers.SubscribersController
{
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;

    public partial class SubscribersControllerTest
    {
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
namespace TokanPages.WebApi.Tests.SubscribersController
{
    using Xunit;
    using FluentAssertions;
    using System.Net;
    using System.Threading.Tasks;

    public partial class SubscribersControllerTest
    {
        [Fact]
        public async Task GivenNoJwt_WhenGetAllSubscribers_ShouldReturnUnauthorized()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/GetAllSubscribers/";
            var httpClient = _webApplicationFactory.CreateClient();

            // Act
            var response = await httpClient.GetAsync(request);
            await EnsureStatusCode(response, HttpStatusCode.Unauthorized);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().BeEmpty();
        }
    }
}
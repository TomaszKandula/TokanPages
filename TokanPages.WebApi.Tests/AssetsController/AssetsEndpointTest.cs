namespace TokanPages.WebApi.Tests.AssetsController
{
    using Xunit;
    using FluentAssertions;
    using System.Net;
    using System.Threading.Tasks;

    public partial class AssetsControllerTest
    {
        [Theory]
        [InlineData("images/icons/__github.png")]
        [InlineData("images/icons/__linkedin.png")]
        public async Task GivenValidBlobName_WhenRequestingAsset_ShouldSucceed(string ABlobName)
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/?ABlobName={ABlobName}";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.OK);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GivenInvalidBlobName_WhenRequestingAsset_ShouldReturnNotFound()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/?ABlobName={DataUtilityService.GetRandomString()}";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.NotFound);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
        }
    }
}
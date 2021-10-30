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
        public async Task GivenValidBlobName_WhenRequestingAsset_ShouldSucceed(string blobName)
        {
            // Arrange
            var request = $"{ApiBaseUrl}/?BlobName={blobName}";

            // Act
            var httpClient = _webApplicationFactory.CreateClient();
            var response = await httpClient.GetAsync(request);
            await EnsureStatusCode(response, HttpStatusCode.OK);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GivenInvalidBlobName_WhenRequestingAsset_ShouldReturnNotFound()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/?BlobName={DataUtilityService.GetRandomString()}";

            // Act
            var httpClient = _webApplicationFactory.CreateClient();
            var response = await httpClient.GetAsync(request);
            await EnsureStatusCode(response, HttpStatusCode.NotFound);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
        }
    }
}
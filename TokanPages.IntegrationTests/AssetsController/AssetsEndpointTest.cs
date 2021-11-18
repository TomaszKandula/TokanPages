namespace TokanPages.IntegrationTests.AssetsController
{
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Backend.Database.Initializer.Data.Articles;

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

        [Fact]
        public async Task GivenCorrectId_WhenGetArticleAsset_ShouldSucceed()
        {
            // Arrange
            var testUserId = Article1.Id;
            var request = $"{ApiBaseUrl}/article/?Id={testUserId}&assetName=image.jpg";
            var httpClient = _webApplicationFactory.CreateClient();

            // Act
            var response = await httpClient.GetAsync(request);
            await EnsureStatusCode(response, HttpStatusCode.OK);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenGetArticleAsset_ShouldReturnNotFound()
        {
            // Arrange
            var testUserId = Guid.NewGuid();
            var request = $"{ApiBaseUrl}/article/?Id={testUserId}&assetName=image.jpg";
            var httpClient = _webApplicationFactory.CreateClient();

            // Act
            var response = await httpClient.GetAsync(request);
            await EnsureStatusCode(response, HttpStatusCode.NotFound);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
        }
    }
}
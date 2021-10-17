namespace TokanPages.WebApi.Tests.ArticlesController
{
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Backend.Database.Initializer.Data.Articles;

    public partial class ArticlesControllerTest
    {
        [Fact]
        public async Task GivenCorrectId_WhenGetArticleImage_ShouldSucceed()
        {
            // Arrange
            var LTestUserId = Article1.Id;
            var LRequest = $"{API_BASE_URL}/images/?AId={LTestUserId}";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.OK);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenGetArticleImage_ShouldReturnNotFound()
        {
            // Arrange
            var LTestUserId = Guid.NewGuid();
            var LRequest = $"{API_BASE_URL}/images/?AId={LTestUserId}";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.NotFound);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
        }
    }
}
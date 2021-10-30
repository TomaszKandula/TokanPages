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
            var testUserId = Article1.Id;
            var request = $"{ApiBaseUrl}/images/?Id={testUserId}";
            var httpClient = _webApplicationFactory.CreateClient();

            // Act
            var response = await httpClient.GetAsync(request);
            await EnsureStatusCode(response, HttpStatusCode.OK);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenGetArticleImage_ShouldReturnNotFound()
        {
            // Arrange
            var testUserId = Guid.NewGuid();
            var request = $"{ApiBaseUrl}/images/?Id={testUserId}";
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
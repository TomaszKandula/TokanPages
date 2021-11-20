namespace TokanPages.IntegrationTests.ArticlesController
{
    using Xunit;
    using Newtonsoft.Json;
    using FluentAssertions;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Queries.Articles;
    using Backend.Database.Initializer.Data.Articles;

    public partial class ArticlesControllerTest
    {
        [Fact]
        public async Task GivenCorrectId_WhenGetArticle_ShouldReturnEntityAsJsonObject()
        {
            // Arrange
            var testUserId = Article1.Id;
            var request = $"{ApiBaseUrl}/GetArticle/{testUserId}/";
            var httpClient = _webApplicationFactory.CreateClient();

            // Act
            var response = await httpClient.GetAsync(request);
            await EnsureStatusCode(response, HttpStatusCode.OK);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();

            var deserialized = JsonConvert.DeserializeObject<GetAllArticlesQueryResult>(content);
            deserialized.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenGetArticle_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var request = $"{ApiBaseUrl}/GetArticle/{Guid.NewGuid()}/";

            // Act
            var response = await httpClient.GetAsync(request);
            await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            content.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
        }
    }
}
using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using TokanPages;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Database.Dummies;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;

namespace Backend.IntegrationTests.Handlers.Articles
{
    public class GetArticleQueryHandlerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;

        public GetArticleQueryHandlerTest(CustomWebApplicationFactory<Startup> AWebAppFactory)
        {
            FWebAppFactory = AWebAppFactory;
        }

        [Fact]
        public async Task GetArticle_WhenIdIsCorrect_ShouldReturnEntityAsJsonObject()
        {
            // Arrange
            var LTestUserId = Article1.FId;
            var LRequest = $"/api/v1/articles/getarticle/{LTestUserId}/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<GetAllArticlesQueryResult>(LContent);
            LDeserialized.Should().NotBeNull();
        }

        [Fact]
        public async Task GetArticle_WhenIdIsIncorrect_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            var LHttpClient = FWebAppFactory.CreateClient();
            var LRequest = $"/api/v1/articles/getarticle/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/";

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
        }
    }
}

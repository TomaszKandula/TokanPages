namespace TokanPages.WebApi.Tests.Controllers.ArticlesController
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Queries.Articles;
    using Backend.Database.Initializer.Data.Articles;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public class GetArticleEndpointTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/articles";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public GetArticleEndpointTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;

        [Fact]
        public async Task GivenCorrectId_WhenGetArticle_ShouldReturnEntityAsJsonObject()
        {
            // Arrange
            var LTestUserId = Article1.FId;
            var LRequest = $"{API_BASE_URL}/GetArticle/{LTestUserId}/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.OK);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<GetAllArticlesQueryResult>(LContent);
            LDeserialized.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenGetArticle_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            var LHttpClient = FWebAppFactory.CreateClient();
            var LRequest = $"{API_BASE_URL}/GetArticle/{Guid.NewGuid()}/";

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
        }
    }
}
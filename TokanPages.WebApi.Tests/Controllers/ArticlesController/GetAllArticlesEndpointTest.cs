namespace TokanPages.WebApi.Tests.Controllers.ArticlesController
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Backend.Cqrs.Handlers.Queries.Articles;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public class GetAllArticlesEndpointTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/articles";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public GetAllArticlesEndpointTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;

        [Fact]
        public async Task WhenGetAllArticles_ShouldReturnCollection()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/GetAllArticles/?AIsPublished=false";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert
                .DeserializeObject<IEnumerable<GetAllArticlesQueryResult>>(LContent)
                .ToList();
            
            LDeserialized.Should().NotBeNullOrEmpty();
            LDeserialized.Should().HaveCountGreaterThan(0);
        }
    }
}
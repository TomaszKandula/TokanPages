using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;
using TokanPages;

namespace Backend.IntegrationTests.Handlers.Articles
{   
    public class GetAllArticlesQueryHandlerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;

        public GetAllArticlesQueryHandlerTest(CustomWebApplicationFactory<Startup> AWebAppFactory)
            => FWebAppFactory = AWebAppFactory;

        [Fact]
        public async Task GetAllArticles_ShouldReturnCollection()
        {
            // Arrange
            const string REQUEST = "/api/v1/articles/getallarticles/?AIsPublished=false";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(REQUEST);

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

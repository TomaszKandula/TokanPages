using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TokanPages;
using TokanPages.Backend.Shared.Dto.Articles;
using TokanPages.Backend.Shared.Resources;

namespace Backend.IntegrationTests.Handlers.Articles
{
    public class RemoveArticleCommandHandlerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;

        public RemoveArticleCommandHandlerTest(CustomWebApplicationFactory<Startup> AWebAppFactory)
            => FWebAppFactory = AWebAppFactory;

        [Fact]
        public async Task RemoveSubscriber_WhenIdIsIncorrect_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            const string REQUEST = "/api/v1/articles/removearticle/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new RemoveArticleDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2")
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
        }
    }
}

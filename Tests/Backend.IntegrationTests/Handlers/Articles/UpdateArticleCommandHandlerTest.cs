using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.TestData;
using TokanPages;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.Dto.Articles;

namespace Backend.IntegrationTests.Handlers.Articles
{
    public class UpdateArticleCommandHandlerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;

        public UpdateArticleCommandHandlerTest(CustomWebApplicationFactory<Startup> AWebAppFactory)
        {
            FWebAppFactory = AWebAppFactory;
        }

        [Fact]
        public async Task UpdateArticle_WhenIdIsIncorrect_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            var LRequest = "/api/v1/articles/updatearticle/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new UpdateArticleDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2"),
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                TextToUpload = DataProvider.GetRandomString(150),
                ImageToUpload = DataProvider.GetRandomString(255).ToBase64Encode(),
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
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

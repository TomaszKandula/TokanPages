namespace TokanPages.WebApi.Tests.Controllers.ArticlesController
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend.Shared.Resources;
    using Backend.Shared.Dto.Articles;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;
    
    public class UpdateArticleLikesEndpointTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/articles";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public UpdateArticleLikesEndpointTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;

        [Fact]
        public async Task GivenInvalidArticleId_WhenUpdateArticleLikes_ShouldReturnBadRequest()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateArticleLikes/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new UpdateArticleLikesDto
            {
                Id = Guid.NewGuid(),
                AddToLikes = 10
            };
            
            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), 
                System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);
            
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
        }
    }
}
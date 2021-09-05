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

    public class UpdateArticleCountEndpointTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/articles";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public UpdateArticleCountEndpointTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;

        [Fact]
        public async Task GivenInvalidArticleId_WhenUpdateArticleCount_ShouldReturnBadRequest()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateArticleCount/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new UpdateArticleCountDto
            {
                Id = Guid.NewGuid()
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
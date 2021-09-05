namespace TokanPages.WebApi.Tests.Controllers.ArticlesController
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend.Core.Extensions;
    using Backend.Shared.Dto.Articles;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public class UpdateArticleContentEndpointTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/articles";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public UpdateArticleContentEndpointTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;
        
        [Fact]
        public async Task GivenNoJwt_WhenUpdateArticleContent_ShouldReturnUnauthorized()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateArticleContent/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new UpdateArticleContentDto
            {
                Id = Guid.NewGuid(),
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(150),
                ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
            };
            
            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), 
                System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
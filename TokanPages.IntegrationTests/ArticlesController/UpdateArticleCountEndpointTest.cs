namespace TokanPages.IntegrationTests.ArticlesController
{
    using Xunit;
    using Newtonsoft.Json;
    using FluentAssertions;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend.Shared.Resources;
    using Backend.Shared.Dto.Articles;

    public partial class ArticlesControllerTest
    {
        [Fact]
        public async Task GivenInvalidArticleId_WhenUpdateArticleCount_ShouldReturnUnprocessableEntity()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/UpdateArticleCount/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

            var payLoad = new UpdateArticleCountDto
            {
                Id = Guid.NewGuid()
            };
            
            var httpClient = _webApplicationFactory.CreateClient();
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), 
                System.Text.Encoding.Default, "application/json");

            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            content.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
        }
    }
}
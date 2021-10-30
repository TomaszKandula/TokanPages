namespace TokanPages.WebApi.Tests.ArticlesController
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
        public async Task GivenInvalidArticleId_WhenUpdateArticleLikes_ShouldReturnBadRequest()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/UpdateArticleLikes/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

            var payLoad = new UpdateArticleLikesDto
            {
                Id = Guid.NewGuid(),
                AddToLikes = 10
            };
            
            var httpClient = _webApplicationFactory.CreateClient();
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), 
                System.Text.Encoding.Default, "application/json");

            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.BadRequest);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            content.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
        }
    }
}
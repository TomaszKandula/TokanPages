namespace TokanPages.WebApi.Tests.ArticlesController
{
    using Xunit;
    using Newtonsoft.Json;
    using FluentAssertions;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend.Shared.Dto.Articles;

    public partial class ArticlesControllerTest
    {
        [Fact]
        public async Task GivenIncorrectIdAndNoJwt_WhenRemoveSubscriber_ShouldReturnUnauthorized()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/RemoveArticle/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

            var payLoad = new RemoveArticleDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2")
            };

            var httpClient = _webApplicationFactory.CreateClient();
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.Unauthorized);

            // Assert
            var result = await response.Content.ReadAsStringAsync();
            result.Should().BeEmpty();
        }
    }
}
namespace TokanPages.WebApi.Tests.ArticlesController
{
    using Xunit;
    using Newtonsoft.Json;
    using FluentAssertions;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend.Core.Extensions;
    using Backend.Shared.Dto.Articles;

    public partial class ArticlesControllerTest
    {
        [Fact]
        public async Task GivenNoJwt_WhenUpdateArticleContent_ShouldReturnUnauthorized()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/UpdateArticleContent/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

            var payLoad = new UpdateArticleContentDto
            {
                Id = Guid.NewGuid(),
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(150),
                ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
            };
            
            var httpClient = _webApplicationFactory.CreateClient();
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), 
                System.Text.Encoding.Default, "application/json");

            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.Unauthorized);

            // Assert
            var result = await response.Content.ReadAsStringAsync();
            result.Should().BeEmpty();
        }
    }
}
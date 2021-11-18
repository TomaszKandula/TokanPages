namespace TokanPages.IntegrationTests.ArticlesController
{
    using Xunit;
    using Newtonsoft.Json;
    using FluentAssertions;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using Backend.Core.Extensions;
    using Backend.Shared.Dto.Articles;

    public partial class ArticlesControllerTest
    {
        [Fact]
        public async Task GivenAllFieldsAreCorrectAsAnonymousUser_WhenAddArticle_ShouldReturnUnauthorized()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/AddArticle/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

            var payLoad = new AddArticleDto
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(150),
                ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
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

        [Fact]
        public async Task GivenAllFieldsAreCorrectAsLoggedUser_WhenAddArticle_ShouldSucceed()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/AddArticle/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

            var payLoad = new AddArticleDto
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(150),
                ImageToUpload = DataUtilityService.GetRandomString(255).ToBase64Encode()
            };

            var httpClient = _webApplicationFactory.CreateClient();
            var tokenExpires = DateTime.Now.AddDays(30);
            var jwt = JwtUtilityService.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), 
                _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.OK);

            // Assert
            var result = await response.Content.ReadAsStringAsync();
            result.Should().NotBeNullOrEmpty();
        }
    }
}
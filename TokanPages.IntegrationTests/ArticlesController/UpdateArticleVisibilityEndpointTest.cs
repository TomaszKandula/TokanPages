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
    using Backend.Shared.Resources;
    using Backend.Shared.Dto.Articles;

    public partial class ArticlesControllerTest
    {
        [Fact]
        public async Task GivenInvalidArticleIdAndValidJwt_WhenUpdateArticleVisibility_ShouldReturnBadRequest()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/UpdateArticleVisibility/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

            var httpClient = _webApplicationFactory.CreateClient();
            var tokenExpires = DateTime.Now.AddDays(30);
            var jwt = JwtUtilityService.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), 
                _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);

            var payLoad = new UpdateArticleVisibilityDto
            {
                Id = Guid.NewGuid(),
                IsPublished = true
            };
            
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), 
                System.Text.Encoding.Default, "application/json");
            
            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.BadRequest);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            content.Should().Contain(ErrorCodes.ACCESS_DENIED);
        }
        
        [Fact]
        public async Task GivenInvalidArticleIdAndInvalidJwt_WhenUpdateArticleVisibility_ShouldReturnForbidden()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/UpdateArticleVisibility/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

            var httpClient = _webApplicationFactory.CreateClient();
            var tokenExpires = DateTime.Now.AddDays(30);
            var jwt = JwtUtilityService.GenerateJwt(tokenExpires, GetInvalidClaimsIdentity(), 
                _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);

            var payLoad = new UpdateArticleVisibilityDto
            {
                Id = Guid.NewGuid(),
                IsPublished = true
            };
            
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), 
                System.Text.Encoding.Default, "application/json");
            
            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.Forbidden);

            // Assert
            var result = await response.Content.ReadAsStringAsync();
            result.Should().BeEmpty();
        }
    }
}
namespace TokanPages.WebApi.Tests.Controllers.ArticlesController
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using Backend.Shared.Resources;
    using Backend.Shared.Dto.Articles;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public partial class ArticlesControllerTest
    {
        [Fact]
        public async Task GivenInvalidArticleIdAndValidJwt_WhenUpdateArticleVisibility_ShouldReturnBadRequest()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateArticleVisibility/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(LTokenExpires, GetValidClaimsIdentity(), 
                FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);

            var LPayLoad = new UpdateArticleVisibilityDto
            {
                Id = Guid.NewGuid(),
                IsPublished = true
            };
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), 
                System.Text.Encoding.Default, "application/json");
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.ACCESS_DENIED);
        }
        
        [Fact]
        public async Task GivenInvalidArticleIdAndInvalidJwt_WhenUpdateArticleVisibility_ShouldReturnForbidden()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateArticleVisibility/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(LTokenExpires, GetInvalidClaimsIdentity(), 
                FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);

            var LPayLoad = new UpdateArticleVisibilityDto
            {
                Id = Guid.NewGuid(),
                IsPublished = true
            };
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), 
                System.Text.Encoding.Default, "application/json");
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.Forbidden);

            // Assert
            var LResult = await LResponse.Content.ReadAsStringAsync();
            LResult.Should().BeEmpty();
        }
    }
}
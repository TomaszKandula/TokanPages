namespace TokanPages.IntegrationTests.UsersController
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
    using Backend.Cqrs.Handlers.Queries.Users;
    using Backend.Database.Initializer.Data.Users;

    public partial class UsersControllerTest
    {
        [Fact]
        public async Task GivenCorrectId_WhenGetUser_ShouldReturnEntityAsJsonObject() 
        {
            // Arrange
            var testUserId = User1.Id;
            var request = $"{ApiBaseUrl}/GetUser/{testUserId}/";
            var newRequest = new HttpRequestMessage(HttpMethod.Get, request);
            
            var httpClient = _webApplicationFactory.CreateClient();
            var tokenExpires = DateTime.Now.AddDays(30);
            var jwt = JwtUtilityService.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            
            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.OK);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            
            var deserialized = JsonConvert.DeserializeObject<GetUserQueryResult>(content);
            deserialized.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenCorrectIdAndInvalidJwt_WhenGetUser_ShouldReturnEntityAsJsonObject() 
        {
            // Arrange
            var testUserId = User1.Id;
            var request = $"{ApiBaseUrl}/GetUser/{testUserId}/";
            var newRequest = new HttpRequestMessage(HttpMethod.Get, request);
            
            var httpClient = _webApplicationFactory.CreateClient();
            var tokenExpires = DateTime.Now.AddDays(30);
            var jwt = JwtUtilityService.GenerateJwt(tokenExpires, GetInvalidClaimsIdentity(), _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            
            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.Forbidden);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().BeEmpty();
        }

        [Fact]
        public async Task GivenInvalidIdAndValidJwt_WhenGetUser_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/GetUser/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/";
            var newRequest = new HttpRequestMessage(HttpMethod.Get, request);
            
            var httpClient = _webApplicationFactory.CreateClient();
            var tokenExpires = DateTime.Now.AddDays(30);
            var jwt = JwtUtilityService.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.BadRequest);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            content.Should().Contain(ErrorCodes.USER_DOES_NOT_EXISTS);
        }
    }
}
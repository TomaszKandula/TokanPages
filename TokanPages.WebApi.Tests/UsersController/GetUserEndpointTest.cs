namespace TokanPages.WebApi.Tests.UsersController
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Queries.Users;
    using Backend.Database.Initializer.Data.Users;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public partial class UsersControllerTest
    {
        [Fact]
        public async Task GivenCorrectId_WhenGetUser_ShouldReturnEntityAsJsonObject() 
        {
            // Arrange
            var LTestUserId = User1.Id;
            var LRequest = $"{API_BASE_URL}/GetUser/{LTestUserId}/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Get, LRequest);
            
            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(LTokenExpires, GetValidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.OK);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            
            var LDeserialized = JsonConvert.DeserializeObject<GetUserQueryResult>(LContent);
            LDeserialized.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenCorrectIdAndInvalidJwt_WhenGetUser_ShouldReturnEntityAsJsonObject() 
        {
            // Arrange
            var LTestUserId = User1.Id;
            var LRequest = $"{API_BASE_URL}/GetUser/{LTestUserId}/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Get, LRequest);
            
            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(LTokenExpires, GetInvalidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.Forbidden);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().BeEmpty();
        }
        
        [Fact]
        public async Task GivenInvalidIdAndValidJwt_WhenGetUser_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/GetUser/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Get, LRequest);
            
            var LHttpClient = FWebAppFactory.CreateClient();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(LTokenExpires, GetValidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.USER_DOES_NOT_EXISTS);
        }
    }
}
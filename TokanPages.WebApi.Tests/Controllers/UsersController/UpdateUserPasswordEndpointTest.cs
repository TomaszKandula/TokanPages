namespace TokanPages.WebApi.Tests.Controllers.UsersController
{
    using Xunit;
    using FluentAssertions;
    using Newtonsoft.Json;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using Backend.Shared.Resources;
    using Backend.Shared.Dto.Users;
    using Backend.Database.Initializer.Data.Users;

    public partial class UsersControllerTest
    {
        [Fact]
        public async Task GivenNewPasswordAndValidJwt_WhenUpdateUserPassword_ShouldFinishSuccessful() 
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateUserPassword/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new UpdateUserPasswordDto
            {
                Id = User2.FId,
                ResetId = null,
                NewPassword = DataUtilityService.GetRandomString()
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            var LTokenExpires = DateTimeService.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(
                LTokenExpires, GetValidClaimsIdentity(nameof(User2)), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.OK);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GivenNewPasswordAndInvalidJwt_WhenUpdateUserPassword_ShouldThrowError() 
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateUserPassword/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new UpdateUserPasswordDto
            {
                Id = User2.FId,
                ResetId = null,
                NewPassword = DataUtilityService.GetRandomString()
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            var LTokenExpires = DateTimeService.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(
                LTokenExpires, GetInvalidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.ACCESS_DENIED); 
        }

        [Fact]
        public async Task GivenNewPasswordAndInvalidUser_WhenUpdateUserPassword_ShouldThrowError() 
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateUserPassword/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new UpdateUserPasswordDto
            {
                Id = Guid.NewGuid(),
                ResetId = null,
                NewPassword = DataUtilityService.GetRandomString()
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            var LTokenExpires = DateTimeService.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(
                LTokenExpires, GetValidClaimsIdentity(nameof(User2)), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.USER_DOES_NOT_EXISTS); 
        }
        
        [Fact]
        public async Task GivenNewPasswordAndInvalidResetId_WhenUpdateUserPassword_ShouldThrowError() 
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateUserPassword/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new UpdateUserPasswordDto
            {
                Id = User2.FId,
                ResetId = Guid.NewGuid(),
                NewPassword = DataUtilityService.GetRandomString()
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            var LTokenExpires = DateTimeService.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(
                LTokenExpires, GetValidClaimsIdentity(nameof(User2)), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.INVALID_RESET_ID); 
        }
    }
}
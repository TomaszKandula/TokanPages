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
    using Backend.Shared.Dto.Users;
    using Backend.Database.Initializer.Data.Users;

    public partial class UsersControllerTest
    {
        [Fact]
        public async Task GivenNewPasswordAndValidJwt_WhenUpdateUserPassword_ShouldFinishSuccessful() 
        {
            // Arrange
            var request = $"{ApiBaseUrl}/UpdateUserPassword/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

            var payLoad = new UpdateUserPasswordDto
            {
                Id = User2.Id,
                ResetId = null,
                NewPassword = DataUtilityService.GetRandomString()
            };

            var httpClient = _webApplicationFactory.CreateClient();
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

            var tokenExpires = DateTimeService.Now.AddDays(30);
            var jwt = JwtUtilityService.GenerateJwt(tokenExpires, GetValidClaimsIdentity(nameof(User2)), 
                _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            
            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.OK);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GivenNewPasswordAndInvalidJwt_WhenUpdateUserPassword_ShouldThrowError() 
        {
            // Arrange
            var request = $"{ApiBaseUrl}/UpdateUserPassword/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

            var payLoad = new UpdateUserPasswordDto
            {
                Id = User2.Id,
                ResetId = null,
                NewPassword = DataUtilityService.GetRandomString()
            };

            var httpClient = _webApplicationFactory.CreateClient();
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

            var tokenExpires = DateTimeService.Now.AddDays(30);
            var jwt = JwtUtilityService.GenerateJwt(tokenExpires, GetInvalidClaimsIdentity(), 
                _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            
            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.Forbidden);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            content.Should().Contain(ErrorCodes.ACCESS_DENIED); 
        }

        [Fact]
        public async Task GivenNewPasswordAndInvalidUser_WhenUpdateUserPassword_ShouldThrowError() 
        {
            // Arrange
            var request = $"{ApiBaseUrl}/UpdateUserPassword/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

            var payLoad = new UpdateUserPasswordDto
            {
                Id = Guid.NewGuid(),
                ResetId = null,
                NewPassword = DataUtilityService.GetRandomString()
            };

            var httpClient = _webApplicationFactory.CreateClient();
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

            var tokenExpires = DateTimeService.Now.AddDays(30);
            var jwt = JwtUtilityService.GenerateJwt(tokenExpires, GetValidClaimsIdentity(nameof(User2)), 
                _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            
            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            content.Should().Contain(ErrorCodes.USER_DOES_NOT_EXISTS); 
        }
        
        [Fact]
        public async Task GivenNewPasswordAndInvalidResetId_WhenUpdateUserPassword_ShouldThrowError() 
        {
            // Arrange
            var request = $"{ApiBaseUrl}/UpdateUserPassword/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

            var payLoad = new UpdateUserPasswordDto
            {
                Id = User2.Id,
                ResetId = Guid.NewGuid(),
                NewPassword = DataUtilityService.GetRandomString()
            };

            var httpClient = _webApplicationFactory.CreateClient();
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

            var tokenExpires = DateTimeService.Now.AddDays(30);
            var jwt = JwtUtilityService.GenerateJwt(tokenExpires, GetValidClaimsIdentity(nameof(User2)), 
                _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            
            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            content.Should().Contain(ErrorCodes.INVALID_RESET_ID); 
        }
    }
}
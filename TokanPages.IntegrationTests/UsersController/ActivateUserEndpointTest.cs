namespace TokanPages.IntegrationTests.UsersController
{
    using Xunit;
    using Newtonsoft.Json;
    using FluentAssertions;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Backend.Shared.Dto.Users;
    using Backend.Shared.Resources;

    public partial class UsersControllerTest
    {
        [Fact]
        public async Task GivenRandomActivationId_WhenActivateUser_ShouldReturnUnprocessableEntity()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/ActivateUser/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);
            var payLoad = new ActivateUserDto { ActivationId = Guid.NewGuid() };

            var httpClient = _webApplicationFactory.CreateClient();
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            content.Should().Contain(ErrorCodes.INVALID_ACTIVATION_ID);
        }
        
        [Fact]
        public async Task GivenrandomActivationId_WhenActivateUserAsLoggedUser_ShouldReturnUnprocessableEntity()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/ActivateUser/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);
            var payLoad = new ActivateUserDto { ActivationId = Guid.NewGuid() };

            var httpClient = _webApplicationFactory.CreateClient();
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

            var tokenExpires = DateTimeService.Now.AddDays(30);
            var jwt = JwtUtilityService.GenerateJwt(
                tokenExpires, GetValidClaimsIdentity(), _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            
            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            content.Should().Contain(ErrorCodes.INVALID_ACTIVATION_ID);
        }
    }
}
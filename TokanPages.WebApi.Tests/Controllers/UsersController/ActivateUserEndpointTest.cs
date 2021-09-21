using TokanPages.Backend.Shared.Resources;

namespace TokanPages.WebApi.Tests.Controllers.UsersController
{
    using Xunit;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Backend.Shared.Dto.Users;

    public partial class UsersControllerTest
    {
        [Fact]
        public async Task GivenActivationId_WhenActivateUserAsNotLogged_ShouldReturnUnauthorized()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/ActivateUser/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);
            var LPayLoad = new ActivateUserDto { ActivationId = Guid.NewGuid() };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.Unauthorized);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().BeEmpty();
        }
        
        [Fact]
        public async Task GivenUnknownActivationId_WhenActivateUserAsAdmin_ShouldReturnBadRequest()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/ActivateUser/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);
            var LPayLoad = new ActivateUserDto { ActivationId = Guid.NewGuid() };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            var LTokenExpires = DateTimeService.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(
                LTokenExpires, GetValidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.INVALID_ACTIVATION_ID);
        }
    }
}
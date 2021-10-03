namespace TokanPages.WebApi.Tests.Controllers.UsersController
{
    using Xunit;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Backend.Shared.Dto.Users;

    public partial class UsersControllerTest
    {
        [Fact]
        public async Task GivenAnyRefreshToken_WhenRevokeUserRefreshTokenAsNotAdmin_ShouldReturnUnauthorized()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/RevokeUserRefreshToken/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new RevokeUserRefreshTokenDto
            {
                RefreshToken = DataUtilityService.GetRandomString(100)
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.Unauthorized);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().BeEmpty();
        }
        
        [Fact]
        public async Task GivenUnknownRefreshToken_WhenRevokeUserRefreshTokenAsAdmin_ShouldReturnBadRequest()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/RevokeUserRefreshToken/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new RevokeUserRefreshTokenDto
            {
                RefreshToken = DataUtilityService.GetRandomString(100)
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            var LTokenExpires = DateTimeService.Now.AddDays(30);
            var LJwt = JwtUtilityService.GenerateJwt(
                LTokenExpires, GetValidClaimsIdentity(), FWebAppFactory.WebSecret, FWebAppFactory.Issuer, FWebAppFactory.Audience);
            
            LNewRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", LJwt);
            
            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
        }
    }
}
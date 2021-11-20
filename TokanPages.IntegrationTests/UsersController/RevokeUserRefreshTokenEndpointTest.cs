namespace TokanPages.IntegrationTests.UsersController
{
    using Xunit;
    using Newtonsoft.Json;
    using FluentAssertions;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using Backend.Shared.Dto.Users;

    public partial class UsersControllerTest
    {
        [Fact]
        public async Task GivenAnyRefreshToken_WhenRevokeUserRefreshTokenAsNotAdmin_ShouldReturnUnauthorized()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/RevokeUserRefreshToken/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

            var payLoad = new RevokeUserRefreshTokenDto
            {
                RefreshToken = DataUtilityService.GetRandomString(100)
            };

            var httpClient = _webApplicationFactory.CreateClient();
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");
            
            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.Unauthorized);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().BeEmpty();
        }
        
        [Fact]
        public async Task GivenUnknownRefreshToken_WhenRevokeUserRefreshTokenAsAdmin_ShouldReturnUnprocessableEntity()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/RevokeUserRefreshToken/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

            var payLoad = new RevokeUserRefreshTokenDto
            {
                RefreshToken = DataUtilityService.GetRandomString(100)
            };

            var httpClient = _webApplicationFactory.CreateClient();
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

            var tokenExpires = DateTimeService.Now.AddDays(30);
            var jwt = JwtUtilityService.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), _webApplicationFactory.WebSecret, 
                _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            
            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
        }
    }
}
namespace TokanPages.WebApi.Tests.UsersController
{
    using Xunit;
    using Newtonsoft.Json;
    using FluentAssertions;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend.Shared;
    using Backend.Shared.Dto.Users;
    using Backend.Shared.Resources;

    public partial class UsersControllerTest
    {
        [Fact]
        public async Task GivenNoRefreshTokensSaved_WhenReAuthenticateUser_ShouldThrowError()
        {
            // Arrange
            var cookieValue = DataUtilityService.GetRandomString(150, "", true);
            var request = $"{ApiBaseUrl}/ReAuthenticateUser/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

            var payLoad = new ReAuthenticateUserDto
            {
                Id = Guid.NewGuid()
            };

            var httpClient = _webApplicationFactory.CreateClient();
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");
            newRequest.Headers.Add("Cookie", $"{Constants.CookieNames.REFRESH_TOKEN}={cookieValue};");

            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.BadRequest);
            
            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            content.Should().Contain(ErrorCodes.INVALID_REFRESH_TOKEN);            
        }
    }
}
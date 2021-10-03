namespace TokanPages.WebApi.Tests.Controllers.UsersController
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend.Shared;
    using Backend.Shared.Dto.Users;
    using Backend.Shared.Resources;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public partial class UsersControllerTest
    {
        [Fact]
        public async Task GivenNoRefreshTokensSaved_WhenReAuthenticateUser_ShouldThrowError()
        {
            // Arrange
            var LCookieValue = DataUtilityService.GetRandomString();
            var LRequest = $"{API_BASE_URL}/ReAuthenticateUser/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new ReAuthenticateUserDto
            {
                Id = Guid.NewGuid()
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");
            LNewRequest.Headers.Add("Cookie", $"{Constants.CookieNames.REFRESH_TOKEN}={LCookieValue};");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);
            
            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.INVALID_REFRESH_TOKEN);            
        }
    }
}
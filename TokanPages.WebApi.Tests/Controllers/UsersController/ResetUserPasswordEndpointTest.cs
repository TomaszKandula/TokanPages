namespace TokanPages.WebApi.Tests.Controllers.UsersController
{
    using Xunit;
    using FluentAssertions;
    using Newtonsoft.Json;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend.Shared.Resources;
    using Backend.Shared.Dto.Users;
    using Backend.Database.Initializer.Data.Users;

    public partial class UsersControllerTest
    {
        [Fact]
        public async Task GivenUserEmail_WhenResetUserPassword_ShouldFinishSuccessful()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/ResetUserPassword/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new ResetUserPasswordDto
            {
                EmailAddress = User3.EMAIL_ADDRESS
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.OK);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GivenInvalidUserEmail_WhenResetUserPassword_ShouldThrowError()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/ResetUserPassword/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new ResetUserPasswordDto
            {
                EmailAddress = DataUtilityService.GetRandomEmail()
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

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
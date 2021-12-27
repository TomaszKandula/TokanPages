namespace TokanPages.IntegrationTests.UsersController;

using Xunit;
using Newtonsoft.Json;
using FluentAssertions;
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
        var request = $"{ApiBaseUrl}/ResetUserPassword/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new ResetUserPasswordDto
        {
            EmailAddress = User3.EmailAddress
        };

        var httpClient = _webApplicationFactory.CreateClient();
        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenInvalidUserEmail_WhenResetUserPassword_ShouldThrowError()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/ResetUserPassword/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new ResetUserPasswordDto
        {
            EmailAddress = DataUtilityService.GetRandomEmail()
        };

        var httpClient = _webApplicationFactory.CreateClient();
        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.USER_DOES_NOT_EXISTS); 
    }
}
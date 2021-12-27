namespace TokanPages.IntegrationTests.UsersController;

using Xunit;
using Newtonsoft.Json;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.Core.Extensions;
using Backend.Shared.Dto.Users;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Users;
using Backend.Database.Initializer.Data.Users;

public partial class UsersControllerTest
{
    [Fact]
    public async Task GivenValidCredentials_WhenAuthenticateUser_ShouldSucceed()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/AuthenticateUser/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new AuthenticateUserDto
        {
            EmailAddress = User1.EmailAddress,
            Password = "user1password"
        };

        // Act
        var httpClient = _webApplicationFactory.CreateClient();
        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        // Assert
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
            
        var deserialized = JsonConvert.DeserializeObject<AuthenticateUserCommandResult>(content);
        deserialized?.UserId.ToString().IsGuid().Should().BeTrue();
        deserialized?.FirstName.Should().Be(User1.FirstName);
        deserialized?.LastName.Should().Be(User1.LastName);
        deserialized?.AliasName.Should().Be(User1.UserAlias);
        deserialized?.ShortBio.Should().Be(User1.ShortBio);
        deserialized?.AvatarName.Should().Be(User1.AvatarName);
        deserialized?.Registered.Should().Be(User1.Registered);
        deserialized?.UserToken.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GivenInvalidCredentials_WhenAuthenticateUser_ShouldThrowError()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/AuthenticateUser/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new AuthenticateUserDto
        {
            EmailAddress = User2.EmailAddress,
            Password = DataUtilityService.GetRandomString()
        };

        // Act
        var httpClient = _webApplicationFactory.CreateClient();
        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        // Assert
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.Forbidden);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.INVALID_CREDENTIALS);
    }
}
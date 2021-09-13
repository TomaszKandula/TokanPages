namespace TokanPages.WebApi.Tests.Controllers.UsersController
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend.Core.Extensions;
    using Backend.Shared.Dto.Users;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Commands.Users;
    using Backend.Database.Initializer.Data.Users;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public partial class UsersControllerTest
    {
        [Fact]
        public async Task GivenValidCredentials_WhenAuthenticateUser_ShouldSucceed()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/AuthenticateUser/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new AuthenticateUserDto
            {
                EmailAddress = User1.EMAIL_ADDRESS,
                Password = "user1password"
            };

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Assert
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.OK);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            
            var LDeserialized = JsonConvert.DeserializeObject<AuthenticateUserCommandResult>(LContent);
            LDeserialized.UserId.ToString().IsGuid().Should().BeTrue();
            LDeserialized.FirstName.Should().Be(User1.FIRST_NAME);
            LDeserialized.LastName.Should().Be(User1.LAST_NAME);
            LDeserialized.AliasName.Should().Be(User1.USER_ALIAS);
            LDeserialized.ShortBio.Should().Be(User1.SHORT_BIO);
            LDeserialized.AvatarName.Should().Be(User1.AVATAR_NAME);
            LDeserialized.Registered.Should().Be(User1.FRegistered);
            LDeserialized.UserToken.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GivenInvalidCredentials_WhenAuthenticateUser_ShouldThrowError()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/AuthenticateUser/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new AuthenticateUserDto
            {
                EmailAddress = User2.EMAIL_ADDRESS,
                Password = DataUtilityService.GetRandomString()
            };

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Assert
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.INVALID_CREDENTIALS);
        }
    }
}
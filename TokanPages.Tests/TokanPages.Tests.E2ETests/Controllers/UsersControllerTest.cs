using System.Net;
using System.Net.Http.Headers;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Application.Users.Queries;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Gateway.Dto.Users;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.UserInfo;
using TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.Users;
using TokanPages.Tests.E2ETests.Helpers;
using Xunit;

namespace TokanPages.Tests.E2ETests.Controllers;

public class UsersControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private readonly CustomWebApplicationFactory<TestStartup> _factory;

    public UsersControllerTest(CustomWebApplicationFactory<TestStartup> factory) => _factory = factory;

    [Fact]
    public async Task GivenValidCredentials_WhenAuthenticateUser_ShouldSucceed()
    {
        // Arrange
        const string uri = $"{BaseUriUsers}/AuthenticateUser/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new AuthenticateUserDto
        {
            EmailAddress = User1.EmailAddress,
            Password = "user1password"
        };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();

        var deserialized = JsonConvert.DeserializeObject<AuthenticateUserCommandResult>(content);
        deserialized?.UserId.ToString().IsGuid().Should().BeTrue();
        deserialized?.FirstName.Should().Be(UserInfo1.FirstName);
        deserialized?.LastName.Should().Be(UserInfo1.LastName);
        deserialized?.AliasName.Should().Be(User1.UserAlias);
        deserialized?.ShortBio.Should().Be(UserInfo1.UserAboutText);
        deserialized?.AvatarName.Should().Be(UserInfo1.UserImageName);
        deserialized?.Registered.Should().Be(User1.CreatedAt);
        deserialized?.UserToken.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GivenInvalidCredentials_WhenAuthenticateUser_ShouldThrowError()
    {
        // Arrange
        const string uri = $"{BaseUriUsers}/AuthenticateUser/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new AuthenticateUserDto
        {
            EmailAddress = User2.EmailAddress,
            Password = DataUtilityService.GetRandomString()
        };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.Forbidden);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.INVALID_CREDENTIALS);
    }

    [Fact]
    public async Task GivenNoRefreshTokensSaved_WhenReAuthenticateUser_ShouldThrowError()
    {
        // Arrange
        const string uri = $"{BaseUriUsers}/ReAuthenticateUser/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        var dto = new ReAuthenticateUserDto { RefreshToken = string.Empty };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);
            
        // Assert
        await EnsureStatusCode(response, HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ValidationCodes.REQUIRED);            
    }

    [Fact]
    public async Task GivenRandomActivationId_WhenActivateUser_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        const string uri = $"{BaseUriUsers}/ActivateUser/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        var dto = new ActivateUserDto { ActivationId = Guid.NewGuid() };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.INVALID_ACTIVATION_ID);
    }

    [Fact]
    public async Task GivenRandomActivationId_WhenActivateUserAsLoggedUser_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        const string uri = $"{BaseUriUsers}/ActivateUser/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        var dto = new ActivateUserDto { ActivationId = Guid.NewGuid() };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        var tokenExpires = DateTimeService.Now.AddDays(30);
        var webSecret = _factory.Configuration.GetValue<string>("Ids_WebSecret");
        var issuer = _factory.Configuration.GetValue<string>("Ids_Issuer");
        var audience = _factory.Configuration.GetValue<string>("Ids_Audience");
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), webSecret, issuer, audience);
            
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.INVALID_ACTIVATION_ID);
    }

    [Fact (Skip = "This test sends email and GitHub actions IPs cannot be whitelisted for now.")]
    public async Task GivenUserEmail_WhenResetUserPassword_ShouldFinishSuccessful()
    {
        // Arrange
        const string uri = $"{BaseUriUsers}/ResetUserPassword/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        var dto = new ResetUserPasswordDto { EmailAddress = User4.EmailAddress };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenInvalidUserEmail_WhenResetUserPassword_ShouldThrowError()
    {
        // Arrange
        const string uri = $"{BaseUriUsers}/ResetUserPassword/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        var dto = new ResetUserPasswordDto { EmailAddress = DataUtilityService.GetRandomEmail() };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.USER_DOES_NOT_EXISTS); 
    }

    [Fact]
    public async Task GivenAnyRefreshToken_WhenRevokeUserRefreshTokenAsNotAdmin_ShouldSucceed()
    {
        // Arrange
        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        const string signInUri = $"{BaseUriUsers}/AuthenticateUser/";
        var signInRequest = new HttpRequestMessage(HttpMethod.Post, signInUri);
        var signinDto = new AuthenticateUserDto
        {
            EmailAddress = User1.EmailAddress,
            Password = "user1password"
        };

        var signInPayload = JsonConvert.SerializeObject(signinDto);
        signInRequest.Content = new StringContent(signInPayload, Encoding.Default, "application/json");

        var signInResponse = await httpClient.SendAsync(signInRequest);
        var signInContent = await signInResponse.Content.ReadAsStringAsync();
        var authenticatedUser = JsonConvert.DeserializeObject<AuthenticateUserCommandResult>(signInContent);

        const string revokeUri = $"{BaseUriUsers}/RevokeUserRefreshToken/";
        var revokeRequest = new HttpRequestMessage(HttpMethod.Post, revokeUri);
        var revokeDto = new RevokeUserRefreshTokenDto { RefreshToken = authenticatedUser?.RefreshToken };

        var payload = JsonConvert.SerializeObject(revokeDto);
        revokeRequest.Content = new StringContent(payload, Encoding.Default, "application/json");
        revokeRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authenticatedUser?.UserToken);

        // Act
        var revokeResponse = await httpClient.SendAsync(revokeRequest);

        // Assert
        await EnsureStatusCode(revokeResponse, HttpStatusCode.OK);
        var content = await revokeResponse.Content.ReadAsStringAsync();
        content.Should().Contain("{}");
    }

    [Fact]
    public async Task GivenUnknownRefreshToken_WhenRevokeUserRefreshTokenAsAdmin_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        const string uri = $"{BaseUriUsers}/RevokeUserRefreshToken/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        var dto = new RevokeUserRefreshTokenDto { RefreshToken = DataUtilityService.GetRandomString(100) };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        var tokenExpires = DateTimeService.Now.AddDays(30);
        var webSecret = _factory.Configuration.GetValue<string>("Ids_WebSecret");
        var issuer = _factory.Configuration.GetValue<string>("Ids_Issuer");
        var audience = _factory.Configuration.GetValue<string>("Ids_Audience");
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), webSecret, issuer, audience);

        await AddWebToken<DatabaseContext>(jwt, _factory.Configuration!);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenValidJwt_WhenGetAllUsers_ShouldReturnCollection() 
    {
        // Arrange
        const string uri = $"{BaseUriUsers}/GetAllUsers/?noCache=true";
        var request = new HttpRequestMessage(HttpMethod.Get, uri);

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var webSecret = _factory.Configuration.GetValue<string>("Ids_WebSecret");
        var issuer = _factory.Configuration.GetValue<string>("Ids_Issuer");
        var audience = _factory.Configuration.GetValue<string>("Ids_Audience");
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), webSecret, issuer, audience);
            
        await AddWebToken<DatabaseContext>(jwt, _factory.Configuration!);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();

        var deserialized = JsonConvert.DeserializeObject<List<GetAllUsersQueryResult>>(content);
        deserialized.Should().NotBeNullOrEmpty();
        deserialized.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public async Task GivenCorrectId_WhenGetUser_ShouldReturnEntityAsJsonObject() 
    {
        // Arrange
        var userId = User1.Id;
        var uri = $"{BaseUriUsers}/{userId}/GetUser/?noCache=true";
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var webSecret = _factory.Configuration.GetValue<string>("Ids_WebSecret");
        var issuer = _factory.Configuration.GetValue<string>("Ids_Issuer");
        var audience = _factory.Configuration.GetValue<string>("Ids_Audience");
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), webSecret, issuer, audience);

        await AddWebToken<DatabaseContext>(jwt, _factory.Configuration!);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();

        var deserialized = JsonConvert.DeserializeObject<GetUserQueryResult>(content);
        deserialized.Should().NotBeNull();
    }

    [Fact]
    public async Task GivenCorrectIdAndInvalidJwt_WhenGetUser_ShouldReturnUnauthorized() 
    {
        // Arrange
        var userId = User1.Id;
        var uri = $"{BaseUriUsers}/{userId}/GetUser/?noCache=true";
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var webSecret = _factory.Configuration.GetValue<string>("Ids_WebSecret");
        var issuer = _factory.Configuration.GetValue<string>("Ids_Issuer");
        var audience = _factory.Configuration.GetValue<string>("Ids_Audience");
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetInvalidClaimsIdentity(), webSecret, issuer, audience);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }

    [Fact]
    public async Task GivenInvalidIdAndValidJwt_WhenGetUser_ShouldReturnJsonObjectWithError()
    {
        // Arrange
        const string uri = $"{BaseUriUsers}/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/GetUser/";
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var webSecret = _factory.Configuration.GetValue<string>("Ids_WebSecret");
        var issuer = _factory.Configuration.GetValue<string>("Ids_Issuer");
        var audience = _factory.Configuration.GetValue<string>("Ids_Audience");
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), webSecret, issuer, audience);

        await AddWebToken<DatabaseContext>(jwt, _factory.Configuration!);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.USER_DOES_NOT_EXISTS);
    }

    [Fact (Skip = "This test sends email and GitHub actions IPs cannot be whitelisted for now.")]
    public async Task GivenAllFieldsAreProvided_WhenAddUser_ShouldReturnNewGuid() 
    {
        // Arrange
        const string testEmail = "test.account@tomkandula.com";
        const string uri = $"{BaseUriUsers}/AddUser/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new AddUserDto 
        { 
            EmailAddress = testEmail,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = DataUtilityService.GetRandomString()
        };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenIncorrectIdNoJwt_WhenUpdateUser_ShouldReturnUnauthorized() 
    {
        // Arrange
        const string uri = $"{BaseUriUsers}/UpdateUser/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new UpdateUserDto
        {
            Id = Guid.NewGuid(),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true
        };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }

    [Fact]
    public async Task GivenNewPasswordAndValidJwt_WhenUpdateUserPassword_ShouldFinishSuccessful() 
    {
        // Arrange
        const string uri = $"{BaseUriUsers}/UpdateUserPassword/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new UpdateUserPasswordDto
        {
            Id = User2.Id,
            ResetId = null,
            NewPassword = "QwertyQwerty#2020*"
        };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        var tokenExpires = DateTimeService.Now.AddDays(30);
        var webSecret = _factory.Configuration.GetValue<string>("Ids_WebSecret");
        var issuer = _factory.Configuration.GetValue<string>("Ids_Issuer");
        var audience = _factory.Configuration.GetValue<string>("Ids_Audience");
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(nameof(User2)), webSecret, issuer, audience);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenNewPasswordAndInvalidJwt_WhenUpdateUserPassword_ShouldThrowError() 
    {
        // Arrange
        const string uri = $"{BaseUriUsers}/UpdateUserPassword/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new UpdateUserPasswordDto
        {
            Id = User2.Id,
            ResetId = null,
            NewPassword = "QwertyQwerty#2020*"
        };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        var tokenExpires = DateTimeService.Now.AddDays(30);
        var webSecret = _factory.Configuration.GetValue<string>("Ids_WebSecret");
        var issuer = _factory.Configuration.GetValue<string>("Ids_Issuer");
        var audience = _factory.Configuration.GetValue<string>("Ids_Audience");
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetInvalidClaimsIdentity(), webSecret, issuer, audience);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.Forbidden);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.ACCESS_DENIED); 
    }

    [Fact]
    public async Task GivenNewPasswordAndInvalidUser_WhenUpdateUserPassword_ShouldThrowError() 
    {
        // Arrange
        const string uri = $"{BaseUriUsers}/UpdateUserPassword/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new UpdateUserPasswordDto
        {
            Id = Guid.NewGuid(),
            ResetId = null,
            NewPassword = "QwertyQwerty#2020*"
        };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        var tokenExpires = DateTimeService.Now.AddDays(30);
        var webSecret = _factory.Configuration.GetValue<string>("Ids_WebSecret");
        var issuer = _factory.Configuration.GetValue<string>("Ids_Issuer");
        var audience = _factory.Configuration.GetValue<string>("Ids_Audience");
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), webSecret, issuer, audience);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.USER_DOES_NOT_EXISTS); 
    }

    [Fact]
    public async Task GivenNewPasswordAndInvalidResetId_WhenUpdateUserPassword_ShouldThrowError() 
    {
        // Arrange
        const string uri = $"{BaseUriUsers}/UpdateUserPassword/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new UpdateUserPasswordDto
        {
            Id = User2.Id,
            ResetId = Guid.NewGuid(),
            NewPassword = "QwertyQwerty#2020*"
        };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        var tokenExpires = DateTimeService.Now.AddDays(30);
        var webSecret = _factory.Configuration.GetValue<string>("Ids_WebSecret");
        var issuer = _factory.Configuration.GetValue<string>("Ids_Issuer");
        var audience = _factory.Configuration.GetValue<string>("Ids_Audience");
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(nameof(User2)), webSecret, issuer, audience);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.INVALID_RESET_ID); 
    }

    [Fact]
    public async Task GivenIncorrectIdAndNoJwt_WhenRemoveUser_ShouldReturnUnauthorized() 
    {
        // Arrange
        const string uri = $"{BaseUriUsers}/RemoveUser/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        var dto = new RemoveUserDto { Id = Guid.NewGuid() };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }
}
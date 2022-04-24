namespace TokanPages.Tests.IntegrationTests.Controllers;

using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Microsoft.AspNetCore.TestHost;
using Backend.Shared;
using Backend.Core.Extensions;
using Backend.Shared.Dto.Users;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Queries.Users;
using Backend.Cqrs.Handlers.Commands.Users;
using Backend.Database.Initializer.Data.Users;
using Factories;

public class UsersControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private const string ApiBaseUrl = "/api/v1.0/users";

    private const string TestRootPath = "TokanPages.Tests/TokanPages.Tests.IntegrationTests";

    private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

    public UsersControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;

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

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");
        var response = await httpClient.SendAsync(newRequest);

        // Assert
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

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");
        var response = await httpClient.SendAsync(newRequest);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.Forbidden);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.INVALID_CREDENTIALS);
    }

    [Fact]
    public async Task GivenNoRefreshTokensSaved_WhenReAuthenticateUser_ShouldThrowError()
    {
        // Arrange
        var cookieValue = DataUtilityService.GetRandomString(150, "", true);
        var request = $"{ApiBaseUrl}/ReAuthenticateUser/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new ReAuthenticateUserDto
        {
            RefreshToken = string.Empty
        };

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();
        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");
        newRequest.Headers.Add("Cookie", $"{Constants.CookieNames.RefreshToken}={cookieValue};");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.Forbidden);
            
        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.INVALID_REFRESH_TOKEN);            
    }

    [Fact]
    public async Task GivenRandomActivationId_WhenActivateUser_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/ActivateUser/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);
        var payLoad = new ActivateUserDto { ActivationId = Guid.NewGuid() };

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.INVALID_ACTIVATION_ID);
    }

    [Fact]
    public async Task GivenrandomActivationId_WhenActivateUserAsLoggedUser_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/ActivateUser/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);
        var payLoad = new ActivateUserDto { ActivationId = Guid.NewGuid() };

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        var tokenExpires = DateTimeService.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(
            tokenExpires, GetValidClaimsIdentity(), _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
        newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            
        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.INVALID_ACTIVATION_ID);
    }

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

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

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

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.USER_DOES_NOT_EXISTS); 
    }

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

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");
            
        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
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

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        var tokenExpires = DateTimeService.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), _webApplicationFactory.WebSecret, 
            _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
        await RegisterTestJwtInDatabase(jwt, _webApplicationFactory.Connection);
            
        newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            
        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenValidJwt_WhenGetAllUsers_ShouldReturnCollection() 
    {
        // Arrange
        var request = $"{ApiBaseUrl}/GetAllUsers/?noCache=true";
        var newRequest = new HttpRequestMessage(HttpMethod.Get, request);
            
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), 
            _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
        await RegisterTestJwtInDatabase(jwt, _webApplicationFactory.Connection);

        newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();

        var deserialized = (JsonConvert.DeserializeObject<IEnumerable<GetAllUsersQueryResult>>(content) ?? Array.Empty<GetAllUsersQueryResult>())
            .ToList();
        deserialized.Should().NotBeNullOrEmpty();
        deserialized.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public async Task GivenCorrectId_WhenGetUser_ShouldReturnEntityAsJsonObject() 
    {
        // Arrange
        var testUserId = User1.Id;
        var request = $"{ApiBaseUrl}/GetUser/{testUserId}/?noCache=true";
        var newRequest = new HttpRequestMessage(HttpMethod.Get, request);
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), 
            _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
        await RegisterTestJwtInDatabase(jwt, _webApplicationFactory.Connection);
            
        newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            
        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
            
        var deserialized = JsonConvert.DeserializeObject<GetUserQueryResult>(content);
        deserialized.Should().NotBeNull();
    }

    [Fact]
    public async Task GivenCorrectIdAndInvalidJwt_WhenGetUser_ShouldReturnUnauthorized() 
    {
        // Arrange
        var testUserId = User1.Id;
        var request = $"{ApiBaseUrl}/GetUser/{testUserId}/?noCache=true";
        var newRequest = new HttpRequestMessage(HttpMethod.Get, request);
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetInvalidClaimsIdentity(), 
            _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
        newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            
        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }

    [Fact]
    public async Task GivenInvalidIdAndValidJwt_WhenGetUser_ShouldReturnJsonObjectWithError()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/GetUser/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/";
        var newRequest = new HttpRequestMessage(HttpMethod.Get, request);
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), 
            _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
        await RegisterTestJwtInDatabase(jwt, _webApplicationFactory.Connection);
            
        newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.USER_DOES_NOT_EXISTS);
    }

    [Fact]
    public async Task GivenAllFieldsAreProvided_WhenAddUser_ShouldReturnNewGuid() 
    {
        // Arrange
        var request = $"{ApiBaseUrl}/AddUser/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new AddUserDto 
        { 
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Password = DataUtilityService.GetRandomString()
        };

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenIncorrectIdNoJwt_WhenUpdateUser_ShouldReturnUnauthorized() 
    {
        // Arrange
        var request = $"{ApiBaseUrl}/UpdateUser/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new UpdateUserDto
        {
            Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2"),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true
        };

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }

    [Fact]
    public async Task GivenNewPasswordAndValidJwt_WhenUpdateUserPassword_ShouldFinishSuccessful() 
    {
        // Arrange
        var request = $"{ApiBaseUrl}/UpdateUserPassword/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new UpdateUserPasswordDto
        {
            Id = User2.Id,
            ResetId = null,
            NewPassword = DataUtilityService.GetRandomString()
        };

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        var tokenExpires = DateTimeService.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(nameof(User2)), 
            _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
        newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            
        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenNewPasswordAndInvalidJwt_WhenUpdateUserPassword_ShouldThrowError() 
    {
        // Arrange
        var request = $"{ApiBaseUrl}/UpdateUserPassword/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new UpdateUserPasswordDto
        {
            Id = User2.Id,
            ResetId = null,
            NewPassword = DataUtilityService.GetRandomString()
        };

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        var tokenExpires = DateTimeService.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetInvalidClaimsIdentity(), 
            _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
        newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            
        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.Forbidden);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.ACCESS_DENIED); 
    }

    [Fact]
    public async Task GivenNewPasswordAndInvalidUser_WhenUpdateUserPassword_ShouldThrowError() 
    {
        // Arrange
        var request = $"{ApiBaseUrl}/UpdateUserPassword/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new UpdateUserPasswordDto
        {
            Id = Guid.NewGuid(),
            ResetId = null,
            NewPassword = DataUtilityService.GetRandomString()
        };

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        var tokenExpires = DateTimeService.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(nameof(User2)), 
            _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
        newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            
        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.USER_DOES_NOT_EXISTS); 
    }

    [Fact]
    public async Task GivenNewPasswordAndInvalidResetId_WhenUpdateUserPassword_ShouldThrowError() 
    {
        // Arrange
        var request = $"{ApiBaseUrl}/UpdateUserPassword/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new UpdateUserPasswordDto
        {
            Id = User2.Id,
            ResetId = Guid.NewGuid(),
            NewPassword = DataUtilityService.GetRandomString()
        };

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        var tokenExpires = DateTimeService.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(nameof(User2)), 
            _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
        newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            
        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.INVALID_RESET_ID); 
    }

    [Fact]
    public async Task GivenIncorrectIdAndNoJwt_WhenRemoveUser_ShouldReturnUnauthorized() 
    {
        // Arrange
        var request = $"{ApiBaseUrl}/RemoveUser/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new RemoveUserDto
        {
            Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2")
        };

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }
}
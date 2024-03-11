using System.Net;
using System.Net.Http.Headers;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.Subscribers;
using TokanPages.Subscribers.Dto.Subscribers;
using TokanPages.Tests.E2ETests.Helpers;
using Xunit;

namespace TokanPages.Tests.E2ETests.Controllers;

public class SubscribersControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private readonly CustomWebApplicationFactory<TestStartup> _factory;

    public SubscribersControllerTest(CustomWebApplicationFactory<TestStartup> factory) => _factory = factory;

    [Fact]
    public async Task GivenNoJwt_WhenGetAllSubscribers_ShouldReturnUnauthorized()
    {
        // Arrange
        const string uri = $"{BaseUriSubscribers}/GetAllSubscribers/?noCache=true";
        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(uri);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }

    [Fact]
    public async Task GivenCorrectIdAndNoJwt_WhenGetSubscriber_ShouldReturnUnauthorized() 
    {
        // Arrange
        var userId = Subscriber1.Id;
        var uri = $"{BaseUriSubscribers}/{userId}/GetSubscriber/?noCache=true";
        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(uri);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenGetSubscriber_ShouldReturnJsonObjectWithError()
    {
        // Arrange
        const string uri = $"{BaseUriSubscribers}/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/GetSubscriber/?noCache=true";
        var request = new HttpRequestMessage(HttpMethod.Get, uri);

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
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
        content.Should().Contain(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
    }

    [Fact]
    public async Task GivenAllFieldsAreCorrect_WhenAddSubscriber_ShouldReturnNewGuid() 
    {
        // Arrange
        const string uri = $"{BaseUriSubscribers}/AddSubscriber/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        var dto = new AddSubscriberDto { Email = DataUtilityService.GetRandomEmail() };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.IsGuid().Should().BeTrue();
    }

    [Fact]
    public async Task GivenIncorrectIdAndNoJwt_WhenUpdateSubscriber_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        const string uri = $"{BaseUriSubscribers}/UpdateSubscriber/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new UpdateSubscriberDto
        {
            Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2"),
            Email = DataUtilityService.GetRandomEmail(),
            Count = null,
            IsActivated = null
        };

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
        content.Should().Contain(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
    }

    [Fact]
    public async Task GivenIncorrectIdAndNoJwt_WhenRemoveSubscriber_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        const string uri = $"{BaseUriSubscribers}/RemoveSubscriber/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        var dto = new RemoveSubscriberDto { Id = Guid.NewGuid() };

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
        content.Should().Contain(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
    }
}
namespace TokanPages.Tests.IntegrationTests.Controllers;

using Xunit;
using Newtonsoft.Json;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.TestHost;
using Backend.Dto.Subscribers;
using Backend.Core.Extensions;
using Backend.Shared.Resources;
using Backend.Database.Initializer.Data.Subscribers;
using Factories;

public class SubscribersControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private const string ApiBaseUrl = "/api/v1.0/subscribers";

    private const string TestRootPath = "TokanPages.Tests/TokanPages.Tests.IntegrationTests";

    private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

    public SubscribersControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;

    [Fact]
    public async Task GivenNoJwt_WhenGetAllSubscribers_ShouldReturnUnauthorized()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/GetAllSubscribers/?noCache=true";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }

    [Fact]
    public async Task GivenCorrectIdAndNoJwt_WhenGetSubscriber_ShouldReturnUnauthorized() 
    {
        // Arrange
        var testUserId = Subscriber1.Id;
        var request = $"{ApiBaseUrl}/GetSubscriber/{testUserId}/?noCache=true";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenGetSubscriber_ShouldReturnJsonObjectWithError()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/GetSubscriber/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/?noCache=true";
        var newRequest = new HttpRequestMessage(HttpMethod.Get, request);
            
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
        newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
    }

    [Fact]
    public async Task GivenAllFieldsAreCorrect_WhenAddSubscriber_ShouldReturnNewGuid() 
    {
        // Arrange
        var request = $"{ApiBaseUrl}/AddSubscriber/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);
        var payLoad = new AddSubscriberDto { Email = DataUtilityService.GetRandomEmail() };

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
        content.IsGuid().Should().BeTrue();
    }

    [Fact]
    public async Task GivenIncorrectIdAndNoJwt_WhenUpdateSubscriber_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/UpdateSubscriber/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new UpdateSubscriberDto
        {
            Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2"),
            Email = DataUtilityService.GetRandomEmail(),
            Count = null,
            IsActivated = null
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
        content.Should().Contain(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
    }

    [Fact]
    public async Task GivenIncorrectIdAndNoJwt_WhenRemoveSubscriber_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/RemoveSubscriber/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new RemoveSubscriberDto
        {
            Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2")
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
        content.Should().Contain(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
    }
}
using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using TokanPages.Backend.Application.Content.Queries;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Tests.IntegrationTests.Helpers;
using Xunit;

namespace TokanPages.Tests.IntegrationTests.Controllers;

public class ContentControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

    public ContentControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;

    [Fact]
    public async Task GivenComponentNameAndType_WhenGetContent_ShouldSucceed()
    {
        // Arrange
        const string componentName = "activateAccount";
        const string componentType = "component";
        const string uri = $"{BaseUriContent}/GetContent/?Name={componentName}&Type={componentType}&noCache=true";

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(uri);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();

        var deserialized = JsonConvert.DeserializeObject<GetContentQueryResult>(content);
        deserialized.Should().NotBeNull();
    }

    [Fact]
    public async Task GivenInvalidComponentNameAndValidType_WhenGetContent_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        var componentName = DataUtilityService.GetRandomString(10, "", true);
        const string componentType = "component";
        var uri = $"{BaseUriContent}/GetContent/?Name={componentName}&Type={componentType}&noCache=true";

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(uri);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(nameof(ErrorCodes.CANNOT_READ_FROM_AZURE_STORAGE));
    }

    [Fact]
    public async Task GivenValidComponentNameAndInvalidType_WhenGetContent_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        const string componentName = "activateAccount";
        var componentType = DataUtilityService.GetRandomString(6, "", true);
        var uri = $"{BaseUriContent}/GetContent/?Name={componentName}&Type={componentType}&noCache=true";

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(uri);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(nameof(ErrorCodes.COMPONENT_TYPE_NOT_SUPPORTED));
    }
}
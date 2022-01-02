namespace TokanPages.Tests.IntegrationTests.Controllers;

using Xunit;
using Newtonsoft.Json;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Queries.Content;
using Factories;

public class ContentControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private const string ApiBaseUrl = "/api/v1.0/content";

    private const string TestRootPath = "TokanPages.Tests/TokanPages.Tests.IntegrationTests";

    private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

    public ContentControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;

    [Fact]
    public async Task GivenComponentNameAndType_WhenGetContent_ShouldSucceed()
    {
        // Arrange
        const string componentName = "activateAccount";
        const string componentType = "component";
            
        var request = $"{ApiBaseUrl}/GetContent/?Name={componentName}&Type={componentType}&noCache=true";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
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
            
        var request = $"{ApiBaseUrl}/GetContent/?Name={componentName}&Type={componentType}&noCache=true";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.COMPONENT_NOT_FOUND);
    }

    [Fact]
    public async Task GivenValidComponentNameAndInvalidType_WhenGetContent_ShouldReturnUnprocessableEntity()
    {
        // Arrange
        const string componentName = "activateAccount";
        var componentType = DataUtilityService.GetRandomString(6, "", true);
            
        var request = $"{ApiBaseUrl}/GetContent/?Name={componentName}&Type={componentType}&noCache=true";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.UnprocessableEntity);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain(ErrorCodes.COMPONENT_TYPE_NOT_SUPPORTED);
    }
}
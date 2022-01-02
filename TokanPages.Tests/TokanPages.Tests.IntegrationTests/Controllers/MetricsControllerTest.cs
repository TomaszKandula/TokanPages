namespace TokanPages.Tests.IntegrationTests.Controllers;

using Xunit;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Backend.Shared;
using Factories;

public class MetricsControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private const string ApiBaseUrl = "/api/v1.0/metrics";

    private const string TestRootPath = "TokanPages.Tests/TokanPages.Tests.IntegrationTests";

    private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

    public MetricsControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;

    [Theory]
    [InlineData("tokanpages-backend")]
    [InlineData("tokanpages-frontend")]
    public async Task GivenAllFieldsAreCorrect_WhenRequestCoverage_ShouldReturnSvgFile(string project)
    {
        // Arrange
        var request = $"{ApiBaseUrl}/?Project={project}&Metric={Constants.MetricNames.Coverage}&noCache=true";
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
    public async Task GivenNoParameters_WhenRequestCoverage_ShouldThrowError()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/?Project=&Metric=&noCache=true";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.BadRequest);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenMissingMetricWithGivenProjectName_WhenRequestCoverage_ShouldThrowError()
    {
        // Arrange
        const string projectName = "tokanpages-backend";
        var request = $"{ApiBaseUrl}/?Project={projectName}&Metric=&noCache=true";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.BadRequest);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenMissingProjectNameWithGivenMetric_WhenRequestCoverage_ShouldThrowError()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/?Project=&Metric={Constants.MetricNames.Coverage}&noCache=true";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.BadRequest);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenProjectNameWithInvalidMetricName_WhenRequestCoverage_ShouldThrowError()
    {
        // Arrange
        var metricName = DataUtilityService.GetRandomString(useAlphabetOnly: true);
        const string projectName = "tokanpages-backend";
        var request = $"{ApiBaseUrl}/?Project={projectName}&Metric={metricName}&noCache=true";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.InternalServerError);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData("tokanpages-backend")]
    [InlineData("tokanpages-frontend")]
    public async Task GivenProjectName_WhenRequestQualityGate_ShouldReturnSvgFile(string project)
    {
        // Arrange
        var request = $"{ApiBaseUrl}/Quality/?Project={project}&noCache=true";
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
    public async Task GivenInvalidProjectName_WhenRequestQualityGate_ShouldReturnSvgWithError()
    {
        // Arrange
        const string invalidProjectName = "InvalidProjectName"; 
        var request = $"{ApiBaseUrl}/Quality/?Project={invalidProjectName}&noCache=true";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Project has not been found");
    }

    [Fact]
    public async Task GivenEmptyProjectName_WhenRequestQualityGate_ShouldThrowError()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/Quality/?Project=&noCache=true";
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.BadRequest);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }
}
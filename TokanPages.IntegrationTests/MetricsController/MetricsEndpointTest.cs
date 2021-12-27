namespace TokanPages.IntegrationTests.MetricsController;

using Xunit;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Backend.Shared;

public partial class MetricsControllerTest
{
    [Theory]
    [InlineData("tokanpages-backend")]
    [InlineData("tokanpages-frontend")]
    public async Task GivenAllFieldsAreCorrect_WhenRequestCoverage_ShouldReturnSvgFile(string project)
    {
        // Arrange
        var request = $"{ApiBaseUrl}/?Project={project}&Metric={Constants.MetricNames.Coverage}&noCache=true";

        // Act
        var httpClient = _webApplicationFactory.CreateClient();
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

        // Act
        var httpClient = _webApplicationFactory.CreateClient();
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

        // Act
        var httpClient = _webApplicationFactory.CreateClient();
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

        // Act
        var httpClient = _webApplicationFactory.CreateClient();
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

        // Act
        var httpClient = _webApplicationFactory.CreateClient();
        var response = await httpClient.GetAsync(request);
        await EnsureStatusCode(response, HttpStatusCode.InternalServerError);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
    }
}
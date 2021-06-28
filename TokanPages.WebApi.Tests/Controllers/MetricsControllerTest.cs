using System.Net;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Core.Services.DataProviderService;

namespace TokanPages.WebApi.Tests.Controllers
{
    public class MetricsControllerTest : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;
        
        private readonly DataProviderService FDataProviderService;

        public MetricsControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory)
        {
            FWebAppFactory = AWebAppFactory;
            FDataProviderService = new DataProviderService();
        }

        [Theory]
        [InlineData("tokanpages-backend")]
        [InlineData("tokanpages-frontend")]
        public async Task GivenAllFieldsAreCorrect_WhenRequestCoverage_ShouldReturnSvgFile(string AProject)
        {
            // Arrange
            var LRequest = $"/api/v1/sonarqube/metrics/?AProject={AProject}&AMetric={Constants.MetricNames.COVERAGE}";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
        }
        
        [Fact]
        public async Task GivenNoParameters_WhenRequestCoverage_ShouldThrowError()
        {
            // Arrange
            const string REQUEST = "/api/v1/sonarqube/metrics/?AProject=&AMetric=";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(REQUEST);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Be("Parameters 'AProject' and 'AMetric' are missing");
        }

        [Fact]
        public async Task GivenMissingMetricWithGivenProjectName_WhenRequestCoverage_ShouldThrowError()
        {
            // Arrange
            const string PROJECT_NAME = "tokanpages-backend";
            var LRequest = $"/api/v1/sonarqube/metrics/?AProject={PROJECT_NAME}&AMetric=";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Be("Parameter 'AMetric' is missing");
        }

        [Fact]
        public async Task GivenMissingProjectNameWithGivenMetric_WhenRequestCoverage_ShouldThrowError()
        {
            // Arrange
            var LRequest = $"/api/v1/sonarqube/metrics/?AProject=&AMetric={Constants.MetricNames.COVERAGE}";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Be("Parameter 'AProject' is missing");
        }

        [Fact]
        public async Task GivenProjectNameWithInvalidMetricName_WhenRequestCoverage_ShouldThrowError()
        {
            // Arrange
            var LMetricName = FDataProviderService.GetRandomString();
            const string PROJECT_NAME = "tokanpages-backend";
            var LRequest = $"/api/v1/sonarqube/metrics/?AProject={PROJECT_NAME}&AMetric={LMetricName}";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Be("Parameter 'AMetric' is invalid.");
        }
        
        [Theory]
        [InlineData("tokanpages-backend")]
        [InlineData("tokanpages-frontend")]
        public async Task GivenProjectName_WhenRequestQualityGate_ShouldReturnSvgFile(string AProject)
        {
            // Arrange
            var LRequest = $"/api/v1/sonarqube/metrics/quality/?AProject={AProject}";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
        }
        
        [Fact]
        public async Task GivenInvalidProjectName_WhenRequestQualityGate_ShouldReturnSvgWithError()
        {
            // Arrange
            const string INVALID_PROJECT_NAME = "InvalidProjectName"; 
            var LRequest = $"/api/v1/sonarqube/metrics/quality/?AProject={INVALID_PROJECT_NAME}";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().Contain("Project has not been found");
        }

        [Fact]
        public async Task GivenEmptyProjectName_WhenRequestQualityGate_ShouldThrowError()
        {
            // Arrange
            var LRequest = $"/api/v1/sonarqube/metrics/quality/?AProject=";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Be("Parameter 'AProject' is missing");
        }
    }
}
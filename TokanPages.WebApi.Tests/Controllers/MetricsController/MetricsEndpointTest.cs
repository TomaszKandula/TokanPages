namespace TokanPages.WebApi.Tests.Controllers.MetricsController
{
    using System.Net;
    using System.Threading.Tasks;
    using Backend.Shared;
    using FluentAssertions;
    using Xunit;

    public class MetricsEndpointTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/sonarqube/metrics";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public MetricsEndpointTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;
        
        [Theory]
        [InlineData("tokanpages-backend")]
        [InlineData("tokanpages-frontend")]
        public async Task GivenAllFieldsAreCorrect_WhenRequestCoverage_ShouldReturnSvgFile(string AProject)
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/?AProject={AProject}&AMetric={Constants.MetricNames.COVERAGE}";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.OK);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
        }
        
        [Fact]
        public async Task GivenNoParameters_WhenRequestCoverage_ShouldThrowError()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/?AProject=&AMetric=";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Be("Parameters 'AProject' and 'AMetric' are missing");
        }

        [Fact]
        public async Task GivenMissingMetricWithGivenProjectName_WhenRequestCoverage_ShouldThrowError()
        {
            // Arrange
            const string PROJECT_NAME = "tokanpages-backend";
            var LRequest = $"{API_BASE_URL}/?AProject={PROJECT_NAME}&AMetric=";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Be("Parameter 'AMetric' is missing");
        }

        [Fact]
        public async Task GivenMissingProjectNameWithGivenMetric_WhenRequestCoverage_ShouldThrowError()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/?AProject=&AMetric={Constants.MetricNames.COVERAGE}";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Be("Parameter 'AProject' is missing");
        }

        [Fact]
        public async Task GivenProjectNameWithInvalidMetricName_WhenRequestCoverage_ShouldThrowError()
        {
            // Arrange
            var LMetricName = DataUtilityService.GetRandomString();
            const string PROJECT_NAME = "tokanpages-backend";
            var LRequest = $"{API_BASE_URL}/?AProject={PROJECT_NAME}&AMetric={LMetricName}";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Be("Parameter 'AMetric' is invalid.");
        }
    }
}
namespace TokanPages.WebApi.Tests.Controllers.MetricsController
{
    using System.Net;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Xunit;

    public class QualityEndpointTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/sonarqube/metrics";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public QualityEndpointTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;
        
        [Theory]
        [InlineData("tokanpages-backend")]
        [InlineData("tokanpages-frontend")]
        public async Task GivenProjectName_WhenRequestQualityGate_ShouldReturnSvgFile(string AProject)
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/Quality/?AProject={AProject}";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.OK);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
        }
        
        [Fact]
        public async Task GivenInvalidProjectName_WhenRequestQualityGate_ShouldReturnSvgWithError()
        {
            // Arrange
            const string INVALID_PROJECT_NAME = "InvalidProjectName"; 
            var LRequest = $"{API_BASE_URL}/Quality/?AProject={INVALID_PROJECT_NAME}";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.OK);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().Contain("Project has not been found");
        }

        [Fact]
        public async Task GivenEmptyProjectName_WhenRequestQualityGate_ShouldThrowError()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/Quality/?AProject=";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);
            
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Be("Parameter 'AProject' is missing");
        }
    }
}
namespace TokanPages.WebApi.Tests.MailerController
{
    using Xunit;

    public partial class MailerControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/Mailer";
        
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;
        
        public MailerControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;
    }
}
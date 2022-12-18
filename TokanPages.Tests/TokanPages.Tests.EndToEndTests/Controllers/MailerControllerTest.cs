using System.Net;
using System.Net.Http.Headers;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Tests.EndToEndTests.Helpers;
using TokanPages.WebApi.Dto.Mailer;
using TokanPages.WebApi.Dto.Mailer.Models;
using Xunit;

namespace TokanPages.Tests.EndToEndTests.Controllers;

public class MailerControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private readonly CustomWebApplicationFactory<TestStartup> _factory;

    public MailerControllerTest(CustomWebApplicationFactory<TestStartup> factory) => _factory = factory;

    [Fact]
    public async Task GivenValidEmail_WhenSendUserMessage_ShouldReturnEmptyJsonObject()
    {
        // Arrange
        const string uri = $"{BaseUriMailer}/SendMessage/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new SendMessageDto
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserEmail = DataUtilityService.GetRandomEmail(),
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string> { string.Empty },
            Subject = "E2E Test / HttpClient / Endpoint",
            Message = $"Test run Id: {Guid.NewGuid()}.",
        };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var payload = JsonConvert.SerializeObject(dto);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Be("{}");
    }

    [Fact]
    public async Task GivenValidEmailsAndValidJwt_WhenSendNewsletter_ShouldReturnEmptyJsonObject()
    {
        // Arrange
        const string testEmail = "contact@tomkandula.com";
        const string uri = $"{BaseUriMailer}/SendNewsletter/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new SendNewsletterDto
        {
            SubscriberInfo = new List<SubscriberInfo>
            {
                new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = testEmail
                }
            },
            Subject = "E2E Test / HttpClient / Endpoint",
            Message = $"<p>Test run Id: {Guid.NewGuid()}.</p><p>Put newsletter content here.</p>"
        };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), 
            _factory.WebSecret, _factory.Issuer, _factory.Audience);

        await RegisterTestJwt(jwt);

        var payload = JsonConvert.SerializeObject(dto);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Be("{}");
    }

    [Fact]
    public async Task GivenValidEmailsAndInvalidJwt_WhenSendNewsletter_ShouldReturnThrownError()
    {
        // Arrange
        const string testEmail = "contact@tomkandula.com";
        const string uri = $"{BaseUriMailer}/SendNewsletter/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new SendNewsletterDto
        {
            SubscriberInfo = new List<SubscriberInfo>
            {
                new()
                {
                    Id    = Guid.NewGuid().ToString(),
                    Email = testEmail
                }
            },
            Subject = "E2E Test / HttpClient / Endpoint",
            Message = $"<p>Test run Id: {Guid.NewGuid()}.</p><p>Put newsletter content here.</p>"
        };

        var httpClient = _factory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetInvalidClaimsIdentity(), 
            _factory.WebSecret, _factory.Issuer, _factory.Audience);

        var payload = JsonConvert.SerializeObject(dto);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        request.Content = new StringContent(payload, Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(request);

        // Assert
        await EnsureStatusCode(response, HttpStatusCode.Unauthorized);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain(nameof(ErrorCodes.INVALID_USER_TOKEN));
    }
}
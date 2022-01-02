namespace TokanPages.Tests.IntegrationTests.Controllers;

using Xunit;
using Newtonsoft.Json;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Microsoft.AspNetCore.TestHost;
using Backend.Shared.Models;
using Backend.Shared.Dto.Mailer;
using Factories;

public class MailerControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private const string ApiBaseUrl = "/api/v1.0/Mailer";

    private const string TestRootPath = "TokanPages.Tests/TokanPages.Tests.IntegrationTests";

    private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

    public MailerControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;

    [Fact]
    public async Task GivenValidEmail_WhenSendUserMessage_ShouldReturnEmptyJsonObject()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/SendMessage/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new SendMessageDto
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserEmail = DataUtilityService.GetRandomEmail(),
            EmailFrom = DataUtilityService.GetRandomEmail(),
            EmailTos = new List<string> { string.Empty },
            Subject = "Integration Test / HttpClient / Endpoint",
            Message = $"Test run Id: {Guid.NewGuid()}.",
        };

        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        // Act
        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Be("{}");
    }

    [Fact]
    public async Task GivenValidEmailsAndValidJwt_WhenSendNewsletter_ShouldReturnEmptyJsonObject()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/SendNewsletter/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new SendNewsletterDto
        {
            SubscriberInfo = new List<SubscriberInfo>
            {
                new()
                {
                    Id    = Guid.NewGuid().ToString(),
                    Email = "tomasz.kandula@gmail.com"
                }
            },
            Subject = "Integration Test / HttpClient / Endpoint",
            Message = $"<p>Test run Id: {Guid.NewGuid()}.</p><p>Put newsletter content here.</p>"
        };
            
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var jwt = JwtUtilityService.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), 
            _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
        await RegisterTestJwtInDatabase(jwt, _webApplicationFactory.Connection);
            
        newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.OK);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Be("{}");
    }

    [Fact]
    public async Task GivenValidEmailsAndInvalidJwt_WhenSendNewsletter_ShouldReturnThrownError()
    {
        // Arrange
        var request = $"{ApiBaseUrl}/SendNewsletter/";
        var newRequest = new HttpRequestMessage(HttpMethod.Post, request);

        var payLoad = new SendNewsletterDto
        {
            SubscriberInfo = new List<SubscriberInfo>
            {
                new()
                {
                    Id    = Guid.NewGuid().ToString(),
                    Email = "tomasz.kandula@gmail.com"
                }
            },
            Subject = "Integration Test / HttpClient / Endpoint",
            Message = $"<p>Test run Id: {Guid.NewGuid()}.</p><p>Put newsletter content here.</p>"
        };
            
        var httpClient = _webApplicationFactory
            .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
            .CreateClient();

        var tokenExpires = DateTime.Now.AddDays(30);
        var jwt = JwtUtilityService.GenerateJwt(tokenExpires, GetInvalidClaimsIdentity(), 
            _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
            
        newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

        // Act
        var response = await httpClient.SendAsync(newRequest);
        await EnsureStatusCode(response, HttpStatusCode.Forbidden);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().BeEmpty();
    }
}
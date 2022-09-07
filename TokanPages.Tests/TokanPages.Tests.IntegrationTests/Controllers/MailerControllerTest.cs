using TokanPages.WebApi.Dto.Mailer;
using TokanPages.WebApi.Dto.Mailer.Models;

namespace TokanPages.Tests.IntegrationTests.Controllers;

using Xunit;
using Newtonsoft.Json;
using FluentAssertions;
using System;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Microsoft.AspNetCore.TestHost;
using Backend.Shared.Resources;
using Factories;

public class MailerControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
{
    private readonly CustomWebApplicationFactory<TestStartup> _webApplicationFactory;

    public MailerControllerTest(CustomWebApplicationFactory<TestStartup> webApplicationFactory) => _webApplicationFactory = webApplicationFactory;

    // [Fact] TODO: redo test
    // public async Task GivenValidEmail_WhenSendUserMessage_ShouldReturnEmptyJsonObject()
    // {
    //     // Arrange
    //     const string uri = $"{BaseUriMailer}/SendMessage/";
    //     var request = new HttpRequestMessage(HttpMethod.Post, uri);
    //
    //     var dto = new SendMessageDto
    //     {
    //         FirstName = DataUtilityService.GetRandomString(),
    //         LastName = DataUtilityService.GetRandomString(),
    //         UserEmail = DataUtilityService.GetRandomEmail(),
    //         EmailFrom = DataUtilityService.GetRandomEmail(),
    //         EmailTos = new List<string> { string.Empty },
    //         Subject = "Integration Test / HttpClient / Endpoint",
    //         Message = $"Test run Id: {Guid.NewGuid()}.",
    //     };
    //
    //     var httpClient = _webApplicationFactory
    //         .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
    //         .CreateClient();
    //
    //     var payload = JsonConvert.SerializeObject(dto);
    //     request.Content = new StringContent(payload, Encoding.Default, "application/json");
    //
    //     // Act
    //     var response = await httpClient.SendAsync(request);
    //
    //     // Assert
    //     await EnsureStatusCode(response, HttpStatusCode.OK);
    //     var content = await response.Content.ReadAsStringAsync();
    //     content.Should().NotBeNullOrEmpty();
    //     content.Should().Be("{}");
    // }

    // [Fact] TODO: redo test
    // public async Task GivenValidEmailsAndValidJwt_WhenSendNewsletter_ShouldReturnEmptyJsonObject()
    // {
    //     // Arrange
    //     const string uri = $"{BaseUriMailer}/SendNewsletter/";
    //     var request = new HttpRequestMessage(HttpMethod.Post, uri);
    //
    //     var dto = new SendNewsletterDto
    //     {
    //         SubscriberInfo = new List<SubscriberInfo>
    //         {
    //             new()
    //             {
    //                 Id = Guid.NewGuid().ToString(),
    //                 Email = "tomasz.kandula@gmail.com"
    //             }
    //         },
    //         Subject = "Integration Test / HttpClient / Endpoint",
    //         Message = $"<p>Test run Id: {Guid.NewGuid()}.</p><p>Put newsletter content here.</p>"
    //     };
    //
    //     var httpClient = _webApplicationFactory
    //         .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(TestRootPath))
    //         .CreateClient();
    //
    //     var tokenExpires = DateTime.Now.AddDays(30);
    //     var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetValidClaimsIdentity(), 
    //         _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);
    //
    //     await RegisterTestJwtInDatabase(jwt, _webApplicationFactory.Connection);
    //
    //     var payload = JsonConvert.SerializeObject(dto);
    //     request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
    //     request.Content = new StringContent(payload, Encoding.Default, "application/json");
    //
    //     // Act
    //     var response = await httpClient.SendAsync(request);
    //
    //     // Assert
    //     await EnsureStatusCode(response, HttpStatusCode.OK);
    //     var content = await response.Content.ReadAsStringAsync();
    //     content.Should().NotBeNullOrEmpty();
    //     content.Should().Be("{}");
    // }

    [Fact]
    public async Task GivenValidEmailsAndInvalidJwt_WhenSendNewsletter_ShouldReturnThrownError()
    {
        // Arrange
        const string uri = $"{BaseUriMailer}/SendNewsletter/";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var dto = new SendNewsletterDto
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
        var jwt = WebTokenUtility.GenerateJwt(tokenExpires, GetInvalidClaimsIdentity(), 
            _webApplicationFactory.WebSecret, _webApplicationFactory.Issuer, _webApplicationFactory.Audience);

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
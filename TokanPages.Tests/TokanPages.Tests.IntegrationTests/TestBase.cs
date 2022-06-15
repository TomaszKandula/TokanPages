namespace TokanPages.Tests.IntegrationTests;

using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.DependencyInjection;
using Backend.Database;
using Backend.Domain.Entities;
using Services.WebTokenService;
using Backend.Core.Utilities.DateTimeService;
using Backend.Database.Initializer.Data.Users;
using Backend.Core.Utilities.DataUtilityService;
using Backend.Database.Initializer.Data.UserInfo;

public class TestBase
{
    private readonly Factories.DatabaseContextFactory _databaseContextFactory;

    protected readonly IDataUtilityService DataUtilityService;

    protected readonly IDateTimeService DateTimeService;

    protected readonly IWebTokenUtility WebTokenUtility;

    protected TestBase()
    {
        var services = new ServiceCollection();
        services.AddSingleton<Factories.DatabaseContextFactory>();
        services.AddScoped<IDataUtilityService, DataUtilityService>();
        services.AddScoped<IWebTokenUtility, WebTokenUtility>();
        services.AddScoped<IDateTimeService, DateTimeService>();

        using var scope = services.BuildServiceProvider(true).CreateScope();
        var serviceProvider = scope.ServiceProvider;

        _databaseContextFactory = serviceProvider.GetRequiredService<Factories.DatabaseContextFactory>();
        DataUtilityService = serviceProvider.GetRequiredService<IDataUtilityService>();
        WebTokenUtility = serviceProvider.GetRequiredService<IWebTokenUtility>();
        DateTimeService = serviceProvider.GetRequiredService<IDateTimeService>();
    }

    private DatabaseContext GetTestDatabaseContext(string connection) =>  _databaseContextFactory.CreateDatabaseContext(connection);

    protected async Task RegisterTestJwtInDatabase(string token, string connection)
    {
        var databaseContext = GetTestDatabaseContext(connection);

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token);
        var securityToken = (JwtSecurityToken)jsonToken;

        var userId = securityToken.Claims.First(claim => claim.Type == "nameid").Value;
        var guid = Guid.Parse(userId);

        var newUserToken = new UserTokens
        {
            UserId = guid,
            Token = token,
            Expires = securityToken.ValidTo,
            Created = securityToken.ValidFrom,
            CreatedByIp = "127.0.0.1",
            Command = nameof(RegisterTestJwtInDatabase)
        };

        await databaseContext.UserTokens.AddAsync(newUserToken);
        await databaseContext.SaveChangesAsync();
    }

    protected static async Task EnsureStatusCode(HttpResponseMessage responseMessage, HttpStatusCode expectedStatusCode)
    {
        if (responseMessage.StatusCode != expectedStatusCode)
        {
            var content = await responseMessage.Content.ReadAsStringAsync();
            var contentText = !string.IsNullOrEmpty(content) ? $"Received content: {content}." : string.Empty;
            throw new Exception($"Expected status code was {expectedStatusCode} but received {responseMessage.StatusCode}. {contentText}");
        }
    }

    protected ClaimsIdentity GetValidClaimsIdentity(string selectedUser = nameof(User1))
    {
        var userId = string.Empty;
        var userFirstName = string.Empty;
        var userLastName = string.Empty;
        var userEmailAddress = string.Empty;

        switch (selectedUser)
        {
            case nameof(User1):
                userId = User1.Id.ToString();
                userFirstName = UserInfo1.FirstName;
                userLastName = UserInfo1.LastName;
                userEmailAddress = User1.EmailAddress;
                break;
                
            case nameof(User2):
                userId = User2.Id.ToString();
                userFirstName = UserInfo2.FirstName;
                userLastName = UserInfo2.LastName;
                userEmailAddress = User2.EmailAddress;
                break;

            case nameof(User3):
                userId = User3.Id.ToString();
                userFirstName = UserInfo3.FirstName;
                userLastName = UserInfo3.LastName;
                userEmailAddress = User3.EmailAddress;
                break;
        }

        var newClaim = new ClaimsIdentity(new []
        {
            new Claim(ClaimTypes.Name, DataUtilityService.GetRandomString()),
            new Claim(ClaimTypes.Role, nameof(Backend.Domain.Enums.Roles.EverydayUser)),
            new Claim(ClaimTypes.Role, nameof(Backend.Domain.Enums.Roles.GodOfAsgard)),
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.GivenName, userFirstName),
            new Claim(ClaimTypes.Surname, userLastName),
            new Claim(ClaimTypes.Email, userEmailAddress)
        });

        return newClaim;
    }

    protected ClaimsIdentity GetInvalidClaimsIdentity() => new (new[]
    {
        new Claim(ClaimTypes.Name, DataUtilityService.GetRandomString()),
        new Claim(ClaimTypes.Role, DataUtilityService.GetRandomString()),
        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.GivenName, DataUtilityService.GetRandomString()),
        new Claim(ClaimTypes.Surname, DataUtilityService.GetRandomString()),
        new Claim(ClaimTypes.Email, DataUtilityService.GetRandomString())
    });
}
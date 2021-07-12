using System;
using System.Security.Claims;
using TokanPages.Backend.Identity.Authorization;
using TokanPages.Backend.Database.Initializer.Data.Users;
using TokanPages.Backend.Shared.Services.DataProviderService;

namespace TokanPages.WebApi.Tests
{
    public class TestBase
    {
        protected DataProviderService DataProviderService { get; }

        protected TestBase() => DataProviderService = new DataProviderService();

        protected ClaimsIdentity GetValidClaimsIdentity()
        {
            return new (new[]
            {
                new Claim(ClaimTypes.Name, DataProviderService.GetRandomString()),
                new Claim(ClaimTypes.Role, nameof(Roles.EverydayUser)),
                new Claim(ClaimTypes.Role, nameof(Roles.GodOfAsgard)),
                new Claim(ClaimTypes.NameIdentifier, User1.FId.ToString()),
                new Claim(ClaimTypes.GivenName, User1.FIRST_NAME),
                new Claim(ClaimTypes.Surname, User1.LAST_NAME),
                new Claim(ClaimTypes.Email, User1.EMAIL_ADDRESS)
            });
        }
        
        protected ClaimsIdentity GetInvalidClaimsIdentity()
        {
            return new (new[]
            {
                new Claim(ClaimTypes.Name, DataProviderService.GetRandomString()),
                new Claim(ClaimTypes.Role, DataProviderService.GetRandomString()),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.GivenName, DataProviderService.GetRandomString()),
                new Claim(ClaimTypes.Surname, DataProviderService.GetRandomString()),
                new Claim(ClaimTypes.Email, DataProviderService.GetRandomString())
            });
        }
    }
}
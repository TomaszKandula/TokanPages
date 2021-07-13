namespace TokanPages.WebApi.Tests
{
    using System;
    using System.Security.Claims;
    using Backend.Identity.Authorization;
    using Backend.Database.Initializer.Data.Users;
    using Backend.Shared.Services.DataProviderService;
    using Backend.Identity.Services.JwtUtilityService;

    public class TestBase
    {
        protected IDataProviderService DataProviderService { get; }

        protected IJwtUtilityService JwtUtilityService { get; }

        protected TestBase()
        {
            DataProviderService = new DataProviderService();
            JwtUtilityService = new JwtUtilityService();
        }

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
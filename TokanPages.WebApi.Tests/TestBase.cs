namespace TokanPages.WebApi.Tests
{
    using System;
    using System.Security.Claims;
    using Backend.Identity.Authorization;
    using Backend.Database.Initializer.Data.Users;
    using Backend.Shared.Services.DataUtilityService;
    using Backend.Identity.Services.JwtUtilityService;

    public class TestBase
    {
        protected IDataUtilityService DataUtilityService { get; }

        protected IJwtUtilityService JwtUtilityService { get; }

        protected TestBase()
        {
            DataUtilityService = new DataUtilityService();
            JwtUtilityService = new JwtUtilityService();
        }

        protected ClaimsIdentity GetValidClaimsIdentity() => new (new[]
        {
            new Claim(ClaimTypes.Name, DataUtilityService.GetRandomString()),
            new Claim(ClaimTypes.Role, nameof(Roles.EverydayUser)),
            new Claim(ClaimTypes.Role, nameof(Roles.GodOfAsgard)),
            new Claim(ClaimTypes.NameIdentifier, User1.FId.ToString()),
            new Claim(ClaimTypes.GivenName, User1.FIRST_NAME),
            new Claim(ClaimTypes.Surname, User1.LAST_NAME),
            new Claim(ClaimTypes.Email, User1.EMAIL_ADDRESS)
        });

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
}
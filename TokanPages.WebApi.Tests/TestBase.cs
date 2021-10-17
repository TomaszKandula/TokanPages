namespace TokanPages.WebApi.Tests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using Backend.Identity.Authorization;
    using Backend.Core.Utilities.DateTimeService;
    using Backend.Database.Initializer.Data.Users;
    using Backend.Core.Utilities.DataUtilityService;
    using Backend.Identity.Services.JwtUtilityService;

    public class TestBase
    {
        protected IDataUtilityService DataUtilityService { get; }

        protected IDateTimeService DateTimeService { get; }
        
        protected IJwtUtilityService JwtUtilityService { get; }

        protected TestBase()
        {
            DataUtilityService = new DataUtilityService();
            DateTimeService = new DateTimeService();
            JwtUtilityService = new JwtUtilityService();
        }

        protected static async Task EnsureStatusCode(HttpResponseMessage AResponseMessage, HttpStatusCode AExpectedStatusCode)
        {
            if (AResponseMessage.StatusCode != AExpectedStatusCode)
            {
                var LContent = await AResponseMessage.Content.ReadAsStringAsync();
                var LContentText = !string.IsNullOrEmpty(LContent) ? $"Received content: {LContent}." : string.Empty;
                throw new Exception($"Expected status code was {AExpectedStatusCode} but received {AResponseMessage.StatusCode}. {LContentText}");
            }
        }

        protected ClaimsIdentity GetValidClaimsIdentity(string ASelectedUser = nameof(User1))
        {
            var LUserId = string.Empty;
            var LUserFirstName = string.Empty;
            var LUserLastName = string.Empty;
            var LUserEmailAddress = string.Empty;

            switch (ASelectedUser)
            {
                case nameof(User1):
                    LUserId = User1.Id.ToString();
                    LUserFirstName = User1.FirstName;
                    LUserLastName = User1.LastName;
                    LUserEmailAddress = User1.EmailAddress;
                    break;
                
                case nameof(User2):
                    LUserId = User2.Id.ToString();
                    LUserFirstName = User2.FirstName;
                    LUserLastName = User2.LastName;
                    LUserEmailAddress = User2.EmailAddress;
                    break;

                case nameof(User3):
                    LUserId = User3.Id.ToString();
                    LUserFirstName = User3.FirstName;
                    LUserLastName = User3.LastName;
                    LUserEmailAddress = User3.EmailAddress;
                    break;
            }

            var LNewClaim = new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.Name, DataUtilityService.GetRandomString()),
                new Claim(ClaimTypes.Role, nameof(Roles.EverydayUser)),
                new Claim(ClaimTypes.Role, nameof(Roles.GodOfAsgard)),
                new Claim(ClaimTypes.NameIdentifier, LUserId),
                new Claim(ClaimTypes.GivenName, LUserFirstName),
                new Claim(ClaimTypes.Surname, LUserLastName),
                new Claim(ClaimTypes.Email, LUserEmailAddress)
            });

            return LNewClaim;
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
}
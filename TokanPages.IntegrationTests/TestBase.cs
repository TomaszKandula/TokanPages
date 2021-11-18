namespace TokanPages.IntegrationTests
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
                    userFirstName = User1.FirstName;
                    userLastName = User1.LastName;
                    userEmailAddress = User1.EmailAddress;
                    break;
                
                case nameof(User2):
                    userId = User2.Id.ToString();
                    userFirstName = User2.FirstName;
                    userLastName = User2.LastName;
                    userEmailAddress = User2.EmailAddress;
                    break;

                case nameof(User3):
                    userId = User3.Id.ToString();
                    userFirstName = User3.FirstName;
                    userLastName = User3.LastName;
                    userEmailAddress = User3.EmailAddress;
                    break;
            }

            var newClaim = new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.Name, DataUtilityService.GetRandomString()),
                new Claim(ClaimTypes.Role, nameof(Roles.EverydayUser)),
                new Claim(ClaimTypes.Role, nameof(Roles.GodOfAsgard)),
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
}
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Identity.Authorization
{
    [ExcludeFromCodeCoverage]
    public static class Claims
    {
        public static string UserAlias => "https://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

        public static string Roles => "https://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        
        public static string UserId => nameof(UserId);
        
        public static string FirstName => nameof(FirstName);
        
        public static string LastName => nameof(LastName);
        
        public static string EmailAddress => nameof(EmailAddress);
    }
}
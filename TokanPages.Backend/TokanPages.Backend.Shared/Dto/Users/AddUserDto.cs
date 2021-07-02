using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Dto.Users
{  
    [ExcludeFromCodeCoverage]
    public class AddUserDto
    {
        public string UserAlias { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string EmailAddress { get; set; }

        public string Password { get; set; }
    }
}

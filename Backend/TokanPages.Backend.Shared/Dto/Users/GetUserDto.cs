using System;

namespace TokanPages.Backend.Shared.Dto.Users
{
    public class GetUserDto
    {
        public string AliasName { get; set; }

        public string AvatarName { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string ShortBio { get; set; }
        
        public DateTime Registered { get; set; }
    }
}

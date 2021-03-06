﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Dto.Users
{
    [ExcludeFromCodeCoverage]
    public class GetUserDto
    {
        public Guid UserId { get; set; }

        public string AliasName { get; set; }

        public string AvatarName { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string ShortBio { get; set; }
        
        public DateTime Registered { get; set; }
    }
}

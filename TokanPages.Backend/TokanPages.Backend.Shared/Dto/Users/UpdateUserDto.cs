﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Dto.Users
{
    [ExcludeFromCodeCoverage]
    public class UpdateUserDto
    {
        public Guid Id { get; set; }

        public string UserAlias { get; set; }
        
        public bool IsActivated { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string EmailAddress { get; set; }
    }
}

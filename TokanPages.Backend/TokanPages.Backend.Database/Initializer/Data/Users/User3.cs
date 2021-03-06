﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Users
{
    [ExcludeFromCodeCoverage]
    public static class User3
    {
        public const string EMAIL_ADDRESS = "dummy@dummy.net";

        public const string USER_ALIAS = "dummy";
        
        public const string FIRST_NAME = "Dummy";
        
        public const string LAST_NAME = "Dummy";
        
        public const bool IS_ACTIVATED = true;
        
        public const string AVATAR_NAME = null;
        
        public const string SHORT_BIO = "Dummy Developer";
        
        public const string CRYPTED_PASSWORD = "$2y$12$S2erEcI.L4AMImaqflEyEOUaoufXW8I.fWUh3JYEecS8vtFHAIZ1S";
        
        public static readonly Guid FId = Guid.Parse("3d047a17-9865-47f1-acb3-53b08539e7c9");
        
        public static readonly DateTime FRegistered = DateTime.Parse("2020-09-12 22:01:33");
        
        public static readonly DateTime? FLastLogged = DateTime.Parse("2020-05-12 15:05:03");
        
        public static readonly DateTime? FLastUpdated = null;
    }
}

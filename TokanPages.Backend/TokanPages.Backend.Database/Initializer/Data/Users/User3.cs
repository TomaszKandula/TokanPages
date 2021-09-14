namespace TokanPages.Backend.Database.Initializer.Data.Users
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class User3
    {
        public const string EMAIL_ADDRESS = "contact@tomkandula.com";

        public const string USER_ALIAS = "dummy";
        
        public const string FIRST_NAME = "Dummy";
        
        public const string LAST_NAME = "Dummy";
        
        public const bool IS_ACTIVATED = true;
        
        public const string AVATAR_NAME = "avatar-default-288.jpeg";
        
        public const string SHORT_BIO = "Dummy Developer";
        
        public const string CRYPTED_PASSWORD = "$2a$12$Bl4ebq6Qi8F4aY5w9wzs7echVwERkAyXxmAua3yUpvUX40DtpCKsK";
        
        public static readonly Guid? FResetId = null;

        public static readonly Guid? FActivationId = null;
        
        public static readonly Guid FId = Guid.Parse("3d047a17-9865-47f1-acb3-53b08539e7c9");
        
        public static readonly DateTime FRegistered = DateTime.Parse("2020-09-12 22:01:33");
        
        public static readonly DateTime? FLastLogged = DateTime.Parse("2020-05-12 15:05:03");
        
        public static readonly DateTime? FLastUpdated = null;

        public static readonly DateTime? FResetIdEnds = null;

        public static readonly DateTime? FActivationIdEnds = null;
    }
}
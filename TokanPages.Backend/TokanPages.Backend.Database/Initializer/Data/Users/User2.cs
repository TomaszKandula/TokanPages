namespace TokanPages.Backend.Database.Initializer.Data.Users
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class User2
    {
        public const string EMAIL_ADDRESS = "tokan@dfds.com";

        public const string USER_ALIAS = "tokan";
        
        public const string FIRST_NAME = "Tom";
        
        public const string LAST_NAME = "Tom";
        
        public const bool IS_ACTIVATED = true;
        
        public const string AVATAR_NAME = "avatar-default-288.jpeg";
        
        public const string SHORT_BIO = "Software Developer";

        public const string CRYPTED_PASSWORD = "$2a$12$Bl4ebq6Qi8F4aY5w9wzs7echVwERkAyXxmAua3yUpvUX40DtpCKsK";

        public static readonly Guid? FResetId = null;

        public static readonly Guid? FActivationId = null;
        
        public static readonly Guid FId = Guid.Parse("d6365db3-d464-4146-857b-d8476f46553c");
        
        public static readonly DateTime FRegistered = DateTime.Parse("2020-01-25 05:09:19");
        
        public static readonly DateTime? FLastLogged = DateTime.Parse("2020-03-22 12:00:15");
        
        public static readonly DateTime? FLastUpdated = DateTime.Parse("2020-05-21 05:09:11");

        public static readonly DateTime? FResetIdEnds = null;

        public static readonly DateTime? FActivationIdEnds = null;
    }
}
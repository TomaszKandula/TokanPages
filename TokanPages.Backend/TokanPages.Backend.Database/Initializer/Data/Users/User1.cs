namespace TokanPages.Backend.Database.Initializer.Data.Users
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class User1
    {
        public const string EMAIL_ADDRESS = "ester.exposito@gmail.com";

        public const string USER_ALIAS = "ester";
        
        public const string FIRST_NAME = "Ester";
        
        public const string LAST_NAME = "Exposito";
        
        public const bool IS_ACTIVATED = true;
        
        public const string AVATAR_NAME = "admin-profile-256.jpg";
        
        public const string SHORT_BIO = "Happy developer";

        public const string CRYPTED_PASSWORD = "$2a$12$Bl4ebq6Qi8F4aY5w9wzs7echVwERkAyXxmAua3yUpvUX40DtpCKsK";

        public static readonly Guid? FResetId = null;

        public static readonly Guid? FActivationId = null;

        public static readonly Guid FId = Guid.Parse("08be222f-dfcd-42db-8509-fd78ef09b912");
        
        public static readonly DateTime FRegistered = DateTime.Parse("2020-01-10 12:15:15");
        
        public static readonly DateTime? FLastLogged = DateTime.Parse("2020-01-10 15:00:33");
        
        public static readonly DateTime? FLastUpdated = null;

        public static readonly DateTime? FResetIdEnds = null;

        public static readonly DateTime? FActivationIdEnds = null;
    }
}
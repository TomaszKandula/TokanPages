namespace TokanPages.Backend.Database.Initializer.Data.Users
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class User1
    {
        public const string EmailAddress = "ester.exposito@gmail.com";

        public const string UserAlias = "ester";
        
        public const string FirstName = "Ester";
        
        public const string LastName = "Exposito";
        
        public const bool IsActivated = true;
        
        public const string AvatarName = "admin-profile-256.jpg";
        
        public const string ShortBio = "Happy developer";

        public const string CryptedPassword = "$2a$12$Bl4ebq6Qi8F4aY5w9wzs7echVwERkAyXxmAua3yUpvUX40DtpCKsK";

        public static readonly Guid? ResetId = null;

        public static readonly Guid? ActivationId = null;

        public static readonly Guid Id = Guid.Parse("08be222f-dfcd-42db-8509-fd78ef09b912");
        
        public static readonly DateTime Registered = DateTime.Parse("2020-01-10 12:15:15");
        
        public static readonly DateTime? LastLogged = DateTime.Parse("2020-01-10 15:00:33");
        
        public static readonly DateTime? LastUpdated = null;

        public static readonly DateTime? ResetIdEnds = null;

        public static readonly DateTime? ActivationIdEnds = null;
    }
}
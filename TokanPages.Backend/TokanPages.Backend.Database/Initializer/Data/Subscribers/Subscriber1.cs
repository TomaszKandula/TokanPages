namespace TokanPages.Backend.Database.Initializer.Data.Subscribers
{   
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class Subscriber1
    {
        public const string Email = "ester1990@gmail.com";

        public const bool IsActivated = false;
        
        public const int Count = 0;

        public static readonly Guid Id = Guid.Parse("098a9c38-c31d-4a29-b5a7-5d02a1a1f7ae");
        
        public static readonly DateTime Registered = DateTime.Parse("2020-01-10 12:15:15");
        
        public static readonly DateTime? LastUpdated = null;
    }
}
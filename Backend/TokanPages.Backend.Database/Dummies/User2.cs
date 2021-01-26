using System;

namespace TokanPages.Backend.Database.Dummies
{
    public class User2
    {
        public static Guid Id = Guid.Parse("d6365db3-d464-4146-857b-d8476f46553c");
        public static DateTime Registered = DateTime.Parse("2020-01-25 05:09:19");
        public static string EmailAddress = "tokan@dfds.com";
        public static string UserAlias = "tokan";
        public static string FirstName = "Tom";
        public static string LastName = "Tom";
        public static bool IsActivated = true;
        public static DateTime? LastLogged = DateTime.Parse("2020-03-22 12:00:15");
        public static DateTime? LastUpdated = DateTime.Parse("2020-05-21 05:09:11");
    }
}

using System;

namespace TokanPages.Backend.Database.Dummies
{
    public class User3
    {
        public static Guid Id = Guid.Parse("3d047a17-9865-47f1-acb3-53b08539e7c9");
        public static DateTime Registered = DateTime.Parse("2020-09-12 22:01:33");
        public static string EmailAddress = "dummy@dummy.net";
        public static string UserAlias = "dummy";
        public static string FirstName = "Dummy";
        public static string LastName = "Dummy";
        public static bool IsActivated = true;
        public static DateTime? LastLogged = DateTime.Parse("2020-05-12 15:05:03");
        public static DateTime? LastUpdated = null;
    }
}

using System;

namespace TokanPages.Backend.Database.Dummies
{

    public class User1
    {
        public static Guid Id = Guid.Parse("08be222f-dfcd-42db-8509-fd78ef09b912");
        public static DateTime Registered = DateTime.Parse("2020-01-10 12:15:15");
        public static string EmailAddress = "ester.exposito@gmail.com";
        public static string UserAlias = "ester";
        public static string FirstName = "Ester";
        public static string LastName = "Exposito";
        public static bool IsActivated = true;
        public static DateTime? LastLogged = DateTime.Parse("2020-01-10 15:00:33");
        public static DateTime? LastUpdated = null;
    }

}

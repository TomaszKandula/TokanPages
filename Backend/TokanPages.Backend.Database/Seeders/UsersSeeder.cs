using System;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Seeders
{

    public class UsersSeeder : IDatabaseContextSeeder
    {

        public class Dummy1 
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

        public class Dummy2 
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

        public class Dummy3
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

        public void Seed(ModelBuilder AModelBuilder)
        {

            AModelBuilder.Entity<Users>()
                .HasData(
                    new Users 
                    { 
                        Id = Dummy1.Id,
                        EmailAddress = Dummy1.EmailAddress,
                        IsActivated = Dummy1.IsActivated,
                        UserAlias = Dummy1.UserAlias,
                        FirstName = Dummy1.FirstName,
                        LastName = Dummy1.LastName,
                        Registered = Dummy1.Registered,
                        LastLogged = Dummy1.LastLogged,
                        LastUpdated = Dummy1.LastUpdated
                    },
                    new Users
                    {
                        Id = Dummy2.Id,
                        EmailAddress = Dummy2.EmailAddress,
                        IsActivated = Dummy2.IsActivated,
                        UserAlias = Dummy2.UserAlias,
                        FirstName = Dummy2.FirstName,
                        LastName = Dummy2.LastName,
                        Registered = Dummy2.Registered,
                        LastLogged = Dummy2.LastLogged,
                        LastUpdated = Dummy2.LastUpdated
                    },
                    new Users
                    {
                        Id = Dummy3.Id,
                        EmailAddress = Dummy3.EmailAddress,
                        IsActivated = Dummy3.IsActivated,
                        UserAlias = Dummy3.UserAlias,
                        FirstName = Dummy3.FirstName,
                        LastName = Dummy3.LastName,
                        Registered = Dummy3.Registered,
                        LastLogged = Dummy3.LastLogged,
                        LastUpdated = Dummy3.LastUpdated
                    }
                );

        }
 
    }

}

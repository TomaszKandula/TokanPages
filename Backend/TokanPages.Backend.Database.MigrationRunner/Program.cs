using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TokanPages.Backend.Database.MigrationRunner
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Database Migration...");

            var LEnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var LBuilder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{LEnvironmentName}.json", true, true);

            var LConfiguration = LBuilder.Build();
            var LConnectionString = LConfiguration.GetConnectionString("DbConnect");
            RunDatabaseContext<DatabaseContext>(LConnectionString, "DbConnect");
        }

        static void RunDatabaseContext<T>(string AConnectionString, string AContextName) where T : DbContext
        {
            Console.WriteLine($"[{AContextName}] Connection string found...");
            Console.WriteLine($"[{AContextName}] Creating Context...");

            var LDatabaseContextOptions = new DbContextOptionsBuilder<T>()
                .UseSqlServer(AConnectionString)
                .Options;

            var LDatabaseContext = (T)Activator.CreateInstance(typeof(T), LDatabaseContextOptions);
            Console.WriteLine($"[{AContextName}] Context created successfully!");

            if (!LDatabaseContext.Database.CanConnect())
            {
                Console.WriteLine($"[{AContextName}] Cannot connect to the database.");
            }

            Console.WriteLine($"[{AContextName}] Database update started...");
            LDatabaseContext.Database.Migrate();
            Console.WriteLine($"[{AContextName}] Finished Database Migration.");
        }
    }
}

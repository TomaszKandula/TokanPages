namespace TokanPages.Backend.Database.Initializer.Data.Subscribers;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public static class Subscriber2
{
    public static readonly Guid Id = Guid.Parse("ec8dd29c-464c-4e7a-897c-ce0ace2619ec");

    public const string Email = "tokan@dfds.com";

    public const bool IsActivated = false;

    public const int Count = 0;

    public static readonly DateTime Registered = DateTime.Parse("2020-01-25 05:09:19");//TODO: to be removed

    public static readonly DateTime? LastUpdated = null;//TODO: to be removed

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-10-03 11:01:05");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}
namespace TokanPages.Backend.Database.Initializer.Data.Subscribers;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public static class Subscriber1
{
    public static readonly Guid Id = Guid.Parse("098a9c38-c31d-4a29-b5a7-5d02a1a1f7ae");

    public const string Email = "ester.exposito@gmail.com";

    public const bool IsActivated = false;

    public const int Count = 0;

    public static readonly DateTime Registered = DateTime.Parse("2020-01-10 12:15:15");//TODO: to be removed

    public static readonly DateTime? LastUpdated = null;//TODO: to be removed

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-10-03 11:01:05");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}
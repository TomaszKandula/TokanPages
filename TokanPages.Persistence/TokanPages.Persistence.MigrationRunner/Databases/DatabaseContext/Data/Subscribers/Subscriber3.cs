using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.Subscribers;

[ExcludeFromCodeCoverage]
public static class Subscriber3
{
    public static readonly Guid Id = Guid.Parse("8a40f1b0-f983-4e51-9bfe-aeb5a5aee1bf");

    public const string Email = "admin@tomkandula.com";

    public const bool IsActivated = false;

    public const int Count = 0;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-10-03 11:01:05");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}
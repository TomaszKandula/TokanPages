using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.Users;

[ExcludeFromCodeCoverage]
public static class User3
{
    public static readonly Guid Id = Guid.Parse("3d047a17-9865-47f1-acb3-53b08539e7c9");

    public const string UserAlias = "jemar";

    public const string EmailAddress = "jenny.marsala@tomkandula.com";

    public const string CryptedPassword = "$2a$12$Bl4ebq6Qi8F4aY5w9wzs7echVwERkAyXxmAua3yUpvUX40DtpCKsK";

    public static readonly Guid? ResetId = null;

    public static readonly DateTime? ResetIdEnds = null;

    public static readonly Guid? ActivationId = null;

    public static readonly DateTime? ActivationIdEnds = null;

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-02-11 15:15:15");

    public static readonly Guid? ModifiedBy = null;

    public static readonly DateTime? ModifiedAt = null;

    public const bool IsActivated = true;

    public const bool IsDeleted = false;
}
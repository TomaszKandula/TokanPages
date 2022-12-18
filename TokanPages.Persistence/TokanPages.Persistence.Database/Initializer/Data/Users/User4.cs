using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.Database.Initializer.Data.Users;

[ExcludeFromCodeCoverage]
public static class User4
{
    public static readonly Guid Id = Guid.Parse("abcb57bd-3a27-47b3-bf0b-fd7ec87569f2");

    public const string UserAlias = "god";

    public const string EmailAddress = "admin@tomkandula.com";

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
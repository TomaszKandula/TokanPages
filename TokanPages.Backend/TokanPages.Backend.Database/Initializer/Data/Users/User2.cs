namespace TokanPages.Backend.Database.Initializer.Data.Users;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public static class User2
{
    public static readonly Guid Id = Guid.Parse("d6365db3-d464-4146-857b-d8476f46553c");

    public const string UserAlias = "vijus";

    public const bool IsActivated = true;

    public const string EmailAddress = "victoria.justice@gmail.com";

    public static readonly DateTime? LastLogged = DateTime.Parse("2020-03-22 12:00:15");

    public const string CryptedPassword = "$2a$12$Bl4ebq6Qi8F4aY5w9wzs7echVwERkAyXxmAua3yUpvUX40DtpCKsK";

    public static readonly Guid? ResetId = null;

    public static readonly DateTime? ResetIdEnds = null;

    public static readonly Guid? ActivationId = null;

    public static readonly DateTime? ActivationIdEnds = null;

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-01-01 02:15:15");

    public static readonly Guid? ModifiedBy = null;

    public static readonly DateTime? ModifiedAt = null;
}
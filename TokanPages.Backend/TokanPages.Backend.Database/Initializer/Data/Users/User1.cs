namespace TokanPages.Backend.Database.Initializer.Data.Users;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public static class User1
{
    public static readonly Guid Id = Guid.Parse("08be222f-dfcd-42db-8509-fd78ef09b912");

    public const string UserAlias = "esexp";

    public const string EmailAddress = "ester.exposito@gmail.com";

    public const string CryptedPassword = "$2a$12$Bl4ebq6Qi8F4aY5w9wzs7echVwERkAyXxmAua3yUpvUX40DtpCKsK";

    public static readonly Guid? ResetId = null;

    public static readonly DateTime? ResetIdEnds = null;

    public static readonly Guid? ActivationId = null;

    public static readonly DateTime? ActivationIdEnds = null;

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-01-15 10:15:15");

    public static readonly Guid? ModifiedBy = null;

    public static readonly DateTime? ModifiedAt = null;

    public const bool IsActivated = false;

    public const bool IsDeleted = false;
}
namespace TokanPages.Backend.Database.Initializer.Data.Users;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public static class User3
{
    public const string EmailAddress = "contact@tomkandula.com";

    public const string UserAlias = "dummy";
        
    public const string FirstName = "Dummy";
        
    public const string LastName = "Dummy";
        
    public const bool IsActivated = true;
        
    public const string AvatarName = "avatar-default-288.jpeg";
        
    public const string ShortBio = "Dummy Developer";
        
    public const string CryptedPassword = "$2a$12$Bl4ebq6Qi8F4aY5w9wzs7echVwERkAyXxmAua3yUpvUX40DtpCKsK";
        
    public static readonly Guid? ResetId = null;

    public static readonly Guid? ActivationId = null;
        
    public static readonly Guid Id = Guid.Parse("3d047a17-9865-47f1-acb3-53b08539e7c9");
        
    public static readonly DateTime Registered = DateTime.Parse("2020-09-12 22:01:33");
        
    public static readonly DateTime? LastLogged = DateTime.Parse("2020-05-12 15:05:03");
        
    public static readonly DateTime? LastUpdated = null;

    public static readonly DateTime? ResetIdEnds = null;

    public static readonly DateTime? ActivationIdEnds = null;
}
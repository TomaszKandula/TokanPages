﻿#nullable enable
namespace TokanPages.Backend.Dto.Users;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Use it when you want to update existing user 
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateUserDto
{
    /// <summary>
    /// Optional
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    public bool IsActivated { get; set; }

    /// <summary>
    /// Optional
    /// </summary>
    public string? UserAlias { get; set; }

    /// <summary>
    /// Optional
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Optional
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Optional
    /// </summary>
    public string? EmailAddress { get; set; }

    /// <summary>
    /// Optional
    /// </summary>
    public string? ShortBio { get; set; }
}
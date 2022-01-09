namespace TokanPages.Backend.Shared.Attributes;

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Domain.Enums;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class AuthorizeUserAttribute : AuthorizeAttribute
{
    public AuthorizeUserAttribute(params Roles[] roles)
        => Roles = string.Join(",", roles);
}
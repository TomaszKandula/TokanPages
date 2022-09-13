using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Shared.Attributes;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class AuthorizeUserAttribute : AuthorizeAttribute
{
    public AuthorizeUserAttribute(params Roles[] roles)
        => Roles = string.Join(",", roles);
}
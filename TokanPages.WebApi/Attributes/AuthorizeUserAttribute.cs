namespace TokanPages.WebApi.Attributes;

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Backend.Domain.Enums;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class AuthorizeUserAttribute : AuthorizeAttribute
{
    public AuthorizeUserAttribute(params Roles[] roles)
        => Roles = string.Join(",", roles);
}
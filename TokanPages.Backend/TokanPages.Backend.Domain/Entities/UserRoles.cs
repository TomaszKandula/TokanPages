namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UserRoles : Entity<Guid>
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public Users UserNavigation { get; set; }

    public Roles RoleNavigation { get; set; }
}
namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using Contracts;

[ExcludeFromCodeCoverage]
public class Roles : Entity<Guid>, IAuditable
{
    [MaxLength(60)]
    public string Name { get; set; }

    [MaxLength(60)]
    public string Description { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public ICollection<DefaultPermissions> DefaultPermissionsNavigation { get; set; } = new HashSet<DefaultPermissions>();

    public ICollection<UserRoles> UserRolesNavigation { get; set; } = new HashSet<UserRoles>();
}
namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class DefaultPermissions : Entity<Guid>
{
    public Guid RoleId { get; set; }
        
    public Roles Role { get; set; } 
        
    public Guid PermissionId { get; set; }
        
    public Permissions Permission { get; set; }
}
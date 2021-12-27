namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class UserPermissions : Entity<Guid>
{
    public Guid UserId { get; set; }
        
    public Users User { get; set; }

    public Guid PermissionId { get; set; }
        
    public Permissions Permission { get; set; }
}
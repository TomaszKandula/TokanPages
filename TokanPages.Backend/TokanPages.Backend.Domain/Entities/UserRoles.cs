using System;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Core.Entities;

namespace TokanPages.Backend.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class UserRoles : Entity<Guid>
    {
        public Guid UserId { get; set; }
        
        public Users User { get; set; }
        
        public Guid RoleId { get; set; }
        
        public Roles Role { get; set; }
    }
}
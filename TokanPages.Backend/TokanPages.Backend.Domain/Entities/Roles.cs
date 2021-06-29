using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using TokanPages.Backend.Core.Entities;

namespace TokanPages.Backend.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Roles : Entity<Guid>
    {
        [MaxLength(60)]
        public string Name { get; set; }
        
        [MaxLength(60)]
        public string Description { get; set; }
        
        public ICollection<DefaultPermissions> DefaultAccessRights { get; set; } = new HashSet<DefaultPermissions>();

        public ICollection<UserRoles> UserRoles { get; set; } = new HashSet<UserRoles>();
    }
}
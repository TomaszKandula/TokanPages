using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using TokanPages.Backend.Core.Entities;

namespace TokanPages.Backend.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Permissions : Entity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<DefaultPermissions> DefaultAccessRights { get; set; } = new HashSet<DefaultPermissions>();
        
        public ICollection<UserPermissions> UserPermissions { get; set; } = new HashSet<UserPermissions>();
    }
}
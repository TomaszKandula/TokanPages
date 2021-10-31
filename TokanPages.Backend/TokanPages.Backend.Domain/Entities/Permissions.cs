namespace TokanPages.Backend.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;

    [ExcludeFromCodeCoverage]
    public class Permissions : Entity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<DefaultPermissions> DefaultPermissions { get; set; } = new HashSet<DefaultPermissions>();
        
        public ICollection<UserPermissions> UserPermissions { get; set; } = new HashSet<UserPermissions>();
    }
}
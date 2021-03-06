﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using TokanPages.Backend.Core.Entities;

namespace TokanPages.Backend.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Subscribers : Entity<Guid>
    {
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        public bool IsActivated { get; set; }

        public int Count { get; set; }

        public DateTime Registered { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}

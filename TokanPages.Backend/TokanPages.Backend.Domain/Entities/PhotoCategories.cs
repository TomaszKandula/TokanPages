namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

[ExcludeFromCodeCoverage]
public class PhotoCategories : Entity<Guid>
{
    [Required]
    [MaxLength(60)]
    public string CategoryName { get; set; }

    public ICollection<Photos> PhotosNavigation { get; set; } = new HashSet<Photos>();
}
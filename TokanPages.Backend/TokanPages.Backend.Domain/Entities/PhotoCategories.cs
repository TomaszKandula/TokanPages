namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using Contracts;

[ExcludeFromCodeCoverage]
public class PhotoCategories : Entity<Guid>, IAuditable
{
    [Required]
    [MaxLength(60)]
    public string CategoryName { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public ICollection<UserPhotos> UserPhotosNavigation { get; set; } = new HashSet<UserPhotos>();
}
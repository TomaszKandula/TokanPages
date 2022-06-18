namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using Contracts;

[ExcludeFromCodeCoverage]
public class UserPhotos : Entity<Guid>, IAuditable
{
    public Guid UserId { get; set; }

    public Guid PhotoGearId { get; set; }

    public Guid PhotoCategoryId { get; set; }

    [MaxLength(500)]
    public string Keywords { get; set; }

    [Required]
    [MaxLength(255)]
    public string PhotoUrl { get; set; }

    public DateTime DateTaken { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Users UserNavigation { get; set; }

    public PhotoGears PhotoGearNavigation { get; set; }

    public PhotoCategories PhotoCategoryNavigation { get; set; }

    public ICollection<Albums> AlbumsNavigation { get; set; } = new HashSet<Albums>();
}
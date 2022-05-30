namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

[ExcludeFromCodeCoverage]
public class Albums : Entity<Guid>
{
    public Guid? UserId { get; set; }

    public Guid? UserPhotoId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    public Users UserNavigation { get; set; }

    public UserPhotos UserPhotoNavigation { get; set; }
}
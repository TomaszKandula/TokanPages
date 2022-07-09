namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using Contracts;

[ExcludeFromCodeCoverage]
public class UserInfo : Entity<Guid>, IAuditable
{
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(255)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(255)]
    public string LastName { get; set; }

    [MaxLength(255)]
    public string UserAboutText { get; set; }

    [MaxLength(255)]
    public string UserImageName { get; set; }

    [MaxLength(255)]
    public string UserVideoName { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Users UserNavigation { get; set; }
}
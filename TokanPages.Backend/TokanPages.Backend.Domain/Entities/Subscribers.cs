namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using Contracts;

[ExcludeFromCodeCoverage]
public class Subscribers : Entity<Guid>, IAuditable
{
    [Required]
    [MaxLength(255)]
    public string Email { get; set; }

    public bool IsActivated { get; set; }

    public int Count { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
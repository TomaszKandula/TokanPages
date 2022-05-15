namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

[ExcludeFromCodeCoverage]
public class Albums : Entity<Guid>
{
    public Guid? UserId { get; set; }

    public Users User { get; set; }

    public Guid PhotoId { get; set; }
        
    public Photos Photo { get; set; }
        
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }
}
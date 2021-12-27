namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

[ExcludeFromCodeCoverage]
public class Articles : Entity<Guid>
{
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [Required]
    [MaxLength(255)]
    public string Description { get; set; }

    public bool IsPublished { get; set; }

    public int ReadCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid UserId { get; set; }

    public Users User { get; set; }

    public ICollection<ArticleLikes> ArticleLikes { get; set; } = new HashSet<ArticleLikes>();

    public ICollection<ArticleCounts> ArticleCounts { get; set; } = new HashSet<ArticleCounts>();
}
﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.User;

namespace TokanPages.Backend.Domain.Entities.Article;

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
    public int TotalLikes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UserId { get; set; }
    [Required]
    [MaxLength(3)]
    public string LanguageIso { get; set; }

    /* Navigation properties */
    public Users Users { get; set; }
    public ICollection<ArticleLikes> ArticleLikes { get; set; } = new HashSet<ArticleLikes>();
    public ICollection<ArticleCounts> ArticleCounts { get; set; } = new HashSet<ArticleCounts>();
}
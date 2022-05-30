namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

[ExcludeFromCodeCoverage]
public class ArticleLikes : Entity<Guid>
{
    [Required]
    public Guid ArticleId { get; set; }

    public Guid? UserId { get; set; }

    [Required]
    [MaxLength(15)]
    public string IpAddress { get; set; }

    public int LikeCount { get; set; }

    public Articles ArticleNavigation { get; set; }

    public Users UserNavigation { get; set; }
}
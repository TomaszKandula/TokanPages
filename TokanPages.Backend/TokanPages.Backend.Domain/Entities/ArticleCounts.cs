namespace TokanPages.Backend.Domain.Entities;

using System;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using Contracts;

[ExcludeFromCodeCoverage]
public class ArticleCounts : Entity<Guid>, IAuditable
{
    [Required]
    public Guid ArticleId { get; set; }

    public Guid? UserId { get; set; }

    [Required]
    [MaxLength(15)]
    public string IpAddress { get; set; }

    public int ReadCount { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Articles ArticleNavigation { get; set; }

    public Users UserNavigation { get; set; }
}
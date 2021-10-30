namespace TokanPages.Backend.Domain.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;
    using Core.Entities;

    [ExcludeFromCodeCoverage]
    public class ArticleCounts : Entity<Guid>
    {
        [Required]
        public Guid ArticleId { get; set; }

        public Guid? UserId { get; set; }

        [Required]
        [MaxLength(15)]
        public string IpAddress { get; set; }

        public int ReadCount { get; set; }

        public Articles Article { get; set; }

        public Users User { get; set; }
    }
}
﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using TokanPages.Backend.Core.Entities;

namespace TokanPages.Backend.Domain.Entities
{
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

        public Articles Article { get; set; }

        public Users User { get; set; }
    }
}

using System;

namespace TokanPages.Backend.Domain.Entities
{
    public partial class Articles
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public int Likes { get; set; }
        public int ReadCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

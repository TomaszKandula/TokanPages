using System;

namespace TokanPages.Backend.Domain.Entities
{
    public partial class Subscribers
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool IsActivated { get; set; }
        public int Count { get; set; }
        public DateTime Registered { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}

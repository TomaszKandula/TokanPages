namespace TokanPages.Backend.Shared.Dto.Subscribers
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class UpdateSubscriberDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }
        
        public bool? IsActivated { get; set; }
        
        public int? Count { get; set; }
    }
}
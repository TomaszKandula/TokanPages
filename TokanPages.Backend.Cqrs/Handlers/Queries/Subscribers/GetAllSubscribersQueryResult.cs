using System;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers
{
    public class GetAllSubscribersQueryResult
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public bool IsActivated { get; set; }

        public int NewsletterCount { get; set; }
    }
}

using System;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers
{
    public class GetSubscriberQueryResult : GetAllSubscribersQueryResult
    {
        public DateTime Registered { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}

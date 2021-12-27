namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers;

using System;

public class GetSubscriberQueryResult : GetAllSubscribersQueryResult
{
    public DateTime Registered { get; set; }

    public DateTime? LastUpdated { get; set; }
}
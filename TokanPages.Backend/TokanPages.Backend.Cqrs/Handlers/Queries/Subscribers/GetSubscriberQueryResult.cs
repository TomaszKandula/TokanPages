namespace TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers;

using System;

public class GetSubscriberQueryResult : GetAllSubscribersQueryResult
{
    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
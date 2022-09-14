namespace TokanPages.Backend.Application.Subscribers.Queries;

public class GetSubscriberQueryResult : GetAllSubscribersQueryResult
{
    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
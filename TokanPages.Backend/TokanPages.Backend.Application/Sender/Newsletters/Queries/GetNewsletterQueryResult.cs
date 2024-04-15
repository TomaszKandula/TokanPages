namespace TokanPages.Backend.Application.Sender.Newsletters.Queries;

public class GetNewsletterQueryResult : GetNewslettersQueryResult
{
    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
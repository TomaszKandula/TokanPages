namespace TokanPages.Backend.Application.Newsletters.Queries;

public class GetNewsletterQueryResult : GetAllNewslettersQueryResult
{
    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
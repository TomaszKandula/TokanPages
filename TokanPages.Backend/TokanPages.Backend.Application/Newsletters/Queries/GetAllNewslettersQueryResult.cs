namespace TokanPages.Backend.Application.Newsletters.Queries;

public class GetAllNewslettersQueryResult
{
    public Guid Id { get; set; }

    public string? Email { get; set; }

    public bool IsActivated { get; set; }

    public int NewsletterCount { get; set; }
}
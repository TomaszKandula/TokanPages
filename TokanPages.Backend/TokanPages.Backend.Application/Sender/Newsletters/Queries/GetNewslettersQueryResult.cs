namespace TokanPages.Backend.Application.Sender.Newsletters.Queries;

public class GetNewslettersQueryResult
{
    public Guid Id { get; set; }

    public string? Email { get; set; }

    public bool IsActivated { get; set; }

    public int NewsletterCount { get; set; }
}
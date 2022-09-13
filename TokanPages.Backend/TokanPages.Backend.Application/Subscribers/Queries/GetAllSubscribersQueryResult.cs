using System;

namespace TokanPages.Backend.Application.Subscribers.Queries;

public class GetAllSubscribersQueryResult
{
    public Guid Id { get; set; }

    public string? Email { get; set; }

    public bool IsActivated { get; set; }

    public int NewsletterCount { get; set; }
}
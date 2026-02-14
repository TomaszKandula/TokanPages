using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Sender.Models;

[ExcludeFromCodeCoverage]
public class UpdateNewsletterDto
{
    public required Guid Id { get; init; }

    public required string Email { get; init; }

    public required bool IsActivated { get; init; }

    public required int Count { get; init; }
}
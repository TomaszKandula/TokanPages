using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Sender.Models;

[ExcludeFromCodeCoverage]
public class UpdateNewsletterDto
{
    public Guid Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public bool IsActivated { get; set; }

    public int Count { get; set; }
}
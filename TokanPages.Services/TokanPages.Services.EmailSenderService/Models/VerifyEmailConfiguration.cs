using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.EmailSenderService.Abstractions;

namespace TokanPages.Services.EmailSenderService.Models;

[ExcludeFromCodeCoverage]
public class VerifyEmailConfiguration : IEmailConfiguration
{
    public Guid MessageId { get; set; }

    public string EmailAddress { get; set; } = "";

    public Guid VerificationId { get; set; }

    public DateTime VerificationIdEnds { get; set; }
}
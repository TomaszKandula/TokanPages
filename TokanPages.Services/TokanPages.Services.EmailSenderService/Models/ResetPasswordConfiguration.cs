using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.EmailSenderService.Abstractions;

namespace TokanPages.Services.EmailSenderService.Models;

[ExcludeFromCodeCoverage]
public class ResetPasswordConfiguration : IEmailConfiguration
{
    public string LanguageId { get; set; } = "";

    public Guid MessageId { get; set; }

    public string EmailAddress { get; set; } = "";
    
    public Guid ResetId { get; set; }    
    
    public DateTime ExpirationDate { get; set; }
}
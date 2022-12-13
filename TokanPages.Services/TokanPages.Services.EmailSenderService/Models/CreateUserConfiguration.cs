using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.EmailSenderService.Abstractions;

namespace TokanPages.Services.EmailSenderService.Models;

[ExcludeFromCodeCoverage]
public class CreateUserConfiguration : IEmailConfiguration
{
    public string EmailAddress { get; set; } = "";
    
    public Guid ActivationId { get; set; }    
    
    public DateTime ActivationIdEnds { get; set; }
}
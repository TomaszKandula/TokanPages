using TokanPages.Services.EmailSenderService.Abstractions;

namespace TokanPages.Services.EmailSenderService.Models;

public class CreateUserConfiguration : IConfiguration
{
    public string EmailAddress { get; set; } = "";
    
    public Guid ActivationId { get; set; }    
    
    public DateTime ActivationIdEnds { get; set; }
}
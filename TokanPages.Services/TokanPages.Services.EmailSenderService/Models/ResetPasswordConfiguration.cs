using TokanPages.Services.EmailSenderService.Abstractions;

namespace TokanPages.Services.EmailSenderService.Models;

public class ResetPasswordConfiguration : IEmailConfiguration
{
    public string EmailAddress { get; set; } = "";
    
    public Guid ResetId { get; set; }    
    
    public DateTime ExpirationDate { get; set; }
}
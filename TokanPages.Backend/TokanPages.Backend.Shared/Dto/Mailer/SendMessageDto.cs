namespace TokanPages.Backend.Shared.Dto.Mailer;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class SendMessageDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
        
    public string UserEmail { get; set; }
        
    public string EmailFrom { get; set; }
        
    public List<string> EmailTos { get; set; }
        
    public string Subject { get; set; }
        
    public string Message { get; set; }
}
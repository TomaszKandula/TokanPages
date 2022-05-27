namespace TokanPages.Backend.Dto.Mailer;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Use it when you want to send email message
/// </summary>
[ExcludeFromCodeCoverage]
public class SendMessageDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    public string LastName { get; set; }
        
    /// <summary>
    /// Mandatory
    /// </summary>
    public string UserEmail { get; set; }
        
    /// <summary>
    /// Mandatory
    /// </summary>
    public string EmailFrom { get; set; }
        
    /// <summary>
    /// Mandatory
    /// </summary>
    public List<string> EmailTos { get; set; }
        
    /// <summary>
    /// Mandatory
    /// </summary>
    public string Subject { get; set; }
        
    /// <summary>
    /// Mandatory
    /// </summary>
    public string Message { get; set; }
}
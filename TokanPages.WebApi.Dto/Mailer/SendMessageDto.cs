using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Mailer;

/// <summary>
/// Use it when you want to send email message
/// </summary>
[ExcludeFromCodeCoverage]
public class SendMessageDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public string FirstName { get; set; } = "";

    /// <summary>
    /// Mandatory
    /// </summary>
    public string LastName { get; set; } = "";
        
    /// <summary>
    /// Mandatory
    /// </summary>
    public string UserEmail { get; set; } = "";
        
    /// <summary>
    /// Mandatory
    /// </summary>
    public string EmailFrom { get; set; } = "";

    /// <summary>
    /// Mandatory
    /// </summary>
    public List<string> EmailTos { get; set; } = new();
        
    /// <summary>
    /// Mandatory
    /// </summary>
    public string Subject { get; set; } = "";
        
    /// <summary>
    /// Mandatory
    /// </summary>
    public string Message { get; set; } = "";
}
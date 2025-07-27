﻿using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Sender.Dto.Mailer;

/// <summary>
/// Use it when you want to send email message.
/// </summary>
[ExcludeFromCodeCoverage]
public class SendMessageDto
{
    /// <summary>
    /// User specific language ID.
    /// </summary>
    public string LanguageId { get; set; } = "";

    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; set; } = "";

    /// <summary>
    /// Last name.
    /// </summary>
    public string LastName { get; set; } = "";
        
    /// <summary>
    /// User email address.
    /// </summary>
    public string UserEmail { get; set; } = "";
        
    /// <summary>
    /// 'From' field.
    /// </summary>
    public string EmailFrom { get; set; } = "";

    /// <summary>
    /// 'To' field.
    /// </summary>
    public List<string> EmailTos { get; set; } = new();
        
    /// <summary>
    /// Subject field.
    /// </summary>
    public string Subject { get; set; } = "";
        
    /// <summary>
    /// Message content.
    /// </summary>
    public string Message { get; set; } = "";

    /// <summary>
    /// Additional associated payload.
    /// </summary>
    public string BusinessData { get; set; } = "";
}
﻿using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Mailer.Commands;
using TokanPages.WebApi.Dto.Mailer;

namespace TokanPages.WebApi.Controllers.Mappers;

/// <summary>
/// Mailer mapper
/// </summary>
[ExcludeFromCodeCoverage]
public static class MailerMapper
{
    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">SendMessageDto</param>
    /// <returns>SendMessageCommand</returns>
    public static SendMessageCommand MapToSendMessageCommand(SendMessageDto model) => new()
    {
        UserEmail = model.UserEmail,
        FirstName = model.FirstName,
        LastName = model.LastName,
        EmailFrom = model.EmailFrom,
        EmailTos = model.EmailTos,
        Subject = model.Subject,
        Message = model.Message
    };

    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">SendNewsletterDto</param>
    /// <returns>SendNewsletterCommand</returns>
    public static SendNewsletterCommand MapToSendNewsletterCommand(SendNewsletterDto model) => new() 
    { 
        SubscriberInfo = model.SubscriberInfo,
        Subject = model.Subject,
        Message = model.Message
    };
}
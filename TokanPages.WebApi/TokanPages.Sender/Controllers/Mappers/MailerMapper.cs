using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Mailer.Commands;
using TokanPages.Sender.Dto.Mailer;

namespace TokanPages.Sender.Controllers.Mappers;

/// <summary>
/// Mailer mapper.
/// </summary>
[ExcludeFromCodeCoverage]
public static class MailerMapper
{
    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
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
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static SendNewsletterCommand MapToSendNewsletterCommand(SendNewsletterDto model) => new() 
    { 
        SubscriberInfo = model.SubscriberInfo?.Select(dto => new Backend.Application.Mailer.Models.SubscriberInfo
        {
            Email = dto.Email,
            Id = dto.Id
        }),
        Subject = model.Subject,
        Message = model.Message
    };
}
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Subscribers.Commands;
using TokanPages.Sender.Dto.Newsletters;

namespace TokanPages.Sender.Controllers.Mappers;

/// <summary>
/// Subscribers mapper.
/// </summary>
[ExcludeFromCodeCoverage]
public static class NewslettersMapper
{
    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static AddSubscriberCommand MapToAddSubscriberCommand(AddNewsletterDto model) => new()
    {
        Email = model.Email
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static UpdateSubscriberCommand MapToUpdateSubscriberCommand(UpdateNewsletterDto model) => new() 
    { 
        Id = model.Id,
        Email = model.Email,
        IsActivated = model.IsActivated,
        Count = model.Count
    };

    /// <summary>
    /// Maps request DTO to given command.
    /// </summary>
    /// <param name="model">Payload object.</param>
    /// <returns>Command object.</returns>
    public static RemoveSubscriberCommand MapToRemoveSubscriberCommand(RemoveNewsletterDto model) => new() 
    { 
        Id = model.Id
    };
}
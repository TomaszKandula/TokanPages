﻿using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Subscribers.Commands;
using TokanPages.WebApi.Dto.Subscribers;

namespace TokanPages.WebApi.Controllers.Mappers;

/// <summary>
/// Subscribers mapper
/// </summary>
[ExcludeFromCodeCoverage]
public static class SubscribersMapper
{
    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">AddSubscriberDto</param>
    /// <returns>AddSubscriberCommand</returns>
    public static AddSubscriberCommand MapToAddSubscriberCommand(AddSubscriberDto model) => new()
    {
        Email = model.Email
    };

    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">UpdateSubscriberDto</param>
    /// <returns>UpdateSubscriberCommand</returns>
    public static UpdateSubscriberCommand MapToUpdateSubscriberCommand(UpdateSubscriberDto model) => new() 
    { 
        Id = model.Id,
        Email = model.Email,
        IsActivated = model.IsActivated,
        Count = model.Count
    };

    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">RemoveSubscriberDto</param>
    /// <returns>RemoveSubscriberCommand</returns>
    public static RemoveSubscriberCommand MapToRemoveSubscriberCommand(RemoveSubscriberDto model) => new() 
    { 
        Id = model.Id
    };
}
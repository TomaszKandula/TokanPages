using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Subscribers.Commands;
using TokanPages.WebApi.Dto.Subscribers;

namespace TokanPages.WebApi.Controllers.Mappers;

[ExcludeFromCodeCoverage]
public static class SubscribersMapper
{
    public static AddSubscriberCommand MapToAddSubscriberCommand(AddSubscriberDto model) => new()
    {
        Email = model.Email
    };

    public static UpdateSubscriberCommand MapToUpdateSubscriberCommand(UpdateSubscriberDto model) => new() 
    { 
        Id = model.Id,
        Email = model.Email,
        IsActivated = model.IsActivated,
        Count = model.Count
    };

    public static RemoveSubscriberCommand MapToRemoveSubscriberCommand(RemoveSubscriberDto model) => new() 
    { 
        Id = model.Id
    };
}
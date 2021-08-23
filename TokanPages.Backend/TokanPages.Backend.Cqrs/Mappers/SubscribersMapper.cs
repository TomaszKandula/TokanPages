namespace TokanPages.Backend.Cqrs.Mappers
{
    using System.Diagnostics.CodeAnalysis;
    using Shared.Dto.Subscribers;
    using Handlers.Commands.Subscribers;

    [ExcludeFromCodeCoverage]
    public static class SubscribersMapper
    {
        public static AddSubscriberCommand MapToAddSubscriberCommand(AddSubscriberDto AModel) => new ()
        {
            Email = AModel.Email
        };

        public static UpdateSubscriberCommand MapToUpdateSubscriberCommand(UpdateSubscriberDto AModel) => new () 
        { 
            Id = AModel.Id,
            Email = AModel.Email,
            IsActivated = AModel.IsActivated,
            Count = AModel.Count
        };

        public static RemoveSubscriberCommand MapToRemoveSubscriberCommand(RemoveSubscriberDto AModel) => new () 
        { 
            Id = AModel.Id
        };
    }
}
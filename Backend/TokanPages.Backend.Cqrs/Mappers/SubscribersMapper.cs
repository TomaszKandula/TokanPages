using TokanPages.Backend.Shared.Dto.Subscribers;
using TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

namespace TokanPages.Backend.Cqrs.Mappers
{

    public static class SubscribersMapper
    {

        public static AddSubscriberCommand MapToAddSubscriberCommand(AddSubscriberRequest AModel) 
        {
            return new AddSubscriberCommand
            {
                Email = AModel.Email
            };
        }

        public static UpdateSubscriberCommand MapToUpdateSubscriberCommand(UpdateSubscriberRequest AModel) 
        {
            return new UpdateSubscriberCommand 
            { 
                Id = AModel.Id,
                Email = AModel.Email,
                IsActivated = AModel.IsActivated,
                Count = AModel.Count
            };            
        }

        public static RemoveSubscriberCommand MapToRemoveSubscriberCommand(RemoveSubscriberRequest AModel) 
        {
            return new RemoveSubscriberCommand 
            { 
                Id = AModel.Id
            };        
        }

    }

}

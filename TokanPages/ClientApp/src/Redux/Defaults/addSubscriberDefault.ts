import { IAddSubscriber } from "../../Redux/States/addSubscriberState";
import { OperationStatuses } from "../../Shared/Enums";

export const AddSubscriberDefault: IAddSubscriber = 
{
    isAddingSubscriber: OperationStatuses.notStarted,
    hasAddedSubscriber: false
}

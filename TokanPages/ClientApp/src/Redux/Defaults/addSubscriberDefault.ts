import { IAddSubscriber } from "../../Redux/States/addSubscriberState";
import { OperationStatus } from "../../Shared/Enums";

export const AddSubscriberDefault: IAddSubscriber = 
{
    isAddingSubscriber: OperationStatus.notStarted,
    hasAddedSubscriber: false,
    attachedErrorObject: { }
}

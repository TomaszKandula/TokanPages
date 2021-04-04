import { IAddSubscriber } from "../../Redux/States/addSubscriberState";
import { OperationStatus } from "../../Shared/enums";

export const AddSubscriberDefault: IAddSubscriber = 
{
    isAddingSubscriber: OperationStatus.notStarted,
    hasAddedSubscriber: false,
    attachedErrorObject: { }
}

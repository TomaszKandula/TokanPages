import { IAddSubscriber } from "../../States/Subscribers/addSubscriberState";
import { OperationStatus } from "../../../Shared/enums";

export const AddSubscriberDefault: IAddSubscriber = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}

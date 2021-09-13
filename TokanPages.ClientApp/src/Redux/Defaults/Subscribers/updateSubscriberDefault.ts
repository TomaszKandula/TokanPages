import { OperationStatus } from "../../../Shared/enums";
import { IUpdateSubscriber } from "../../../Redux/States/Subscribers/updateSubscriberState";

export const UpdateSubscriberDefault: IUpdateSubscriber = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}

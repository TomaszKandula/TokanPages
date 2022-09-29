import { OperationStatus } from "../../../Shared/enums";
import { IRemoveSubscriber } from "../../States/Subscribers/removeSubscriberState";

export const RemoveSubscriberDefault: IRemoveSubscriber = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}

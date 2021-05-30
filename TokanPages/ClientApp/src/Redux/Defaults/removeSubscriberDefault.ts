import { OperationStatus } from "../../Shared/enums";
import { IRemoveSubscriber } from "../../Redux/States/removeSubscriberState";

export const RemoveSubscriberDefault: IRemoveSubscriber = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}

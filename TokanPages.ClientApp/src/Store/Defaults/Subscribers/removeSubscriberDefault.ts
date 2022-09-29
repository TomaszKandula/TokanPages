import { OperationStatus } from "../../../Shared/enums";
import { IRemoveSubscriber } from "../../States";

export const RemoveSubscriberDefault: IRemoveSubscriber = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}

import { OperationStatus } from "../../../Shared/enums";
import { ISubscriberRemove } from "../../States";

export const RemoveSubscriberDefault: ISubscriberRemove = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}

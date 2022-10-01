import { OperationStatus } from "../../../Shared/enums";
import { ISubscriberRemove } from "../../States";

export const SubscriberRemove: ISubscriberRemove = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}

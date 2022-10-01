import { OperationStatus } from "../../../Shared/enums";
import { ISubscriberUpdate } from "../../States";

export const SubscriberUpdate: ISubscriberUpdate = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}

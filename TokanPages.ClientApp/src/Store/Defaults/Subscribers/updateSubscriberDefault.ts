import { OperationStatus } from "../../../Shared/enums";
import { ISubscriberUpdate } from "../../States";

export const UpdateSubscriberDefault: ISubscriberUpdate = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}

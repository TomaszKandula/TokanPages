import { OperationStatus } from "../../../Shared/enums";
import { IUpdateSubscriber } from "../../States";

export const UpdateSubscriberDefault: IUpdateSubscriber = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}

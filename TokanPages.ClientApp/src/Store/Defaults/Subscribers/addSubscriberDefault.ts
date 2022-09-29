import { IAddSubscriber } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const AddSubscriberDefault: IAddSubscriber = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}

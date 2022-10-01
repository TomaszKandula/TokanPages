import { ISubscriberAdd } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const SubscriberAdd: ISubscriberAdd = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}

import { ISubscriberAdd } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const AddSubscriberDefault: ISubscriberAdd = 
{
    operationStatus: OperationStatus.notStarted,
    attachedErrorObject: { }
}

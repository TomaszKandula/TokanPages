import { OperationStatus } from "../../../Shared/enums";
import { ISubscriberRemove } from "../../States";

export const SubscriberRemove: ISubscriberRemove = 
{
    status: OperationStatus.notStarted,
    response: { }
}

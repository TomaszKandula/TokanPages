import { SubscriberAddState } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const SubscriberAdd: SubscriberAddState = 
{
    status: OperationStatus.notStarted,
    response: { }
}

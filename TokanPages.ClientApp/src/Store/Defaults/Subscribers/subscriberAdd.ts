import { ISubscriberAdd } from "../../States";
import { OperationStatus } from "../../../Shared/enums";

export const SubscriberAdd: ISubscriberAdd = 
{
    status: OperationStatus.notStarted,
    response: { }
}

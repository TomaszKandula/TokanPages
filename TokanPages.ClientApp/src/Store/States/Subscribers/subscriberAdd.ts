import { OperationStatus } from "../../../Shared/enums";

export interface SubscriberAddState
{
    status: OperationStatus;
    response?: any;
}

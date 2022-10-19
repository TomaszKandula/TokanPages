import { OperationStatus } from "../../../Shared/enums";

export interface ISubscriberUpdate
{
    status: OperationStatus;
    response?: any;
}

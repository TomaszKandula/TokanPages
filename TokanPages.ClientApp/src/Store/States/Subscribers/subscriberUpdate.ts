import { OperationStatus } from "../../../Shared/enums";

export interface ISubscriberUpdate
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;
}

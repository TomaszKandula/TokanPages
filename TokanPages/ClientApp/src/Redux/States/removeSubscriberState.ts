import { OperationStatus } from "Shared/enums";

export interface IRemoveSubscriber
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;
}

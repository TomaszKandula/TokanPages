import { OperationStatus } from "../../../Shared/enums";

export interface IUpdateSubscriber
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;
}

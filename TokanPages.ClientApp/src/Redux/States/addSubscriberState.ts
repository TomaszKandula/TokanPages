import { OperationStatus } from "../../Shared/enums";

export interface IAddSubscriber
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;
}

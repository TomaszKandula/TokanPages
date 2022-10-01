import { OperationStatus } from "../../../Shared/enums";

export interface IApplicationMessage
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;
}

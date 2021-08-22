import { OperationStatus } from "../../../Shared/enums";

export interface ISendMessage
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;
}

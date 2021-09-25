import { OperationStatus } from "../../../Shared/enums";

export interface IActivateAccount
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;  
}
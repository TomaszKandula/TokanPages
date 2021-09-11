import { OperationStatus } from "../../../Shared/enums";

export interface IResetUserPassword
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;  
}
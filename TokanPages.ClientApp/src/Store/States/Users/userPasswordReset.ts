import { OperationStatus } from "../../../Shared/enums";

export interface IUserPasswordReset
{
    operationStatus: OperationStatus;
    attachedErrorObject: any;  
}